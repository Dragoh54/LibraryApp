using Azure.Core;
using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel.Enums;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.Services;

public class UserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IPasswordHasher hasher, IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _passwordHasher = hasher;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(string username, string email, string password, Role role = Role.User)
    {
        var candidate = await _unitOfWork.UserRepository.GetByEmail(email);

        if(candidate is not null)
        {
            throw new Exception("User with this email already exists!");
        }

        var hashedPassword = _passwordHasher.Generate(password);

        var user = new UserEntity(Guid.NewGuid(), username, email, hashedPassword, role);

        await _unitOfWork.UserRepository.Add(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _unitOfWork.UserRepository.GetByEmail(email);

        if (user is null)
        {
            throw new Exception("Cannot found user with this email");
        }

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (!result)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvider.GenerateAccessToken(user);
        var refreshToken = _jwtProvider.GenerateRefreshToken(user);
        
        if (refreshToken == null)
        {
            throw new Exception("Failed to generate refresh token.");
        }
        await _unitOfWork.RefreshTokenRepository.Add(refreshToken);

        return token;
    }

    public async Task Logout(HttpContext httpContext)
    {
        var token = httpContext.Request.Cookies["tasty-cookies"];

        if (token is null)
        {
            throw new Exception("You are not logged in");
        }
        
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

        if (userId is null)
        {
            throw new Exception("This user is not logged in");
        }

        var refreshToken = _unitOfWork.RefreshTokenRepository.GetByUserId(Guid.Parse(userId)).Result;

        if (refreshToken is null)
        {
            throw new Exception("Refresh token not found");
        }

        refreshToken.IsUsed = true;
        refreshToken.WhenUsed = DateTime.UtcNow;

        await _unitOfWork.RefreshTokenRepository.Update(refreshToken);
        await _unitOfWork.RefreshTokenRepository.SaveAsync(); 

        _unitOfWork.RefreshTokenRepository.Update(refreshToken);
        httpContext.Response.Cookies.Delete("tasty-cookies");
    }
    
    public async Task<string> Refresh(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokenRepository.GetByToken(refreshToken);
        
        if (token is null)
        {
            throw new Exception("Invalid refresh token");
        }
    
        if (token.IsUsed)
        {
            throw new Exception("This token has already been used");
        }
        
        await _unitOfWork.RefreshTokenRepository.SaveAsync();
        
        var user = await _unitOfWork.UserRepository.Get(token.UserId);
    
        if (user is null)
        {
            throw new Exception("Cannot found user with this token");
        }
        
        var newAccessToken = _jwtProvider.GenerateAccessToken(user);
    
        return newAccessToken;
    }
}
