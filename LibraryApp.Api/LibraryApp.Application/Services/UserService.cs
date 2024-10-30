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
using Microsoft.Extensions.Options;

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

    public async Task<(string, string)> Login(string email, string password)
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
        
        if (refreshToken is null || token is null)
        {
            throw new Exception("Failed to generate tokens.");
        }
        
        await _unitOfWork.RefreshTokenRepository.Add(refreshToken);

        return (token, refreshToken.Id.ToString());
    }

    public async Task Logout(HttpContext httpContext)
    {
        var token = httpContext.Request.Cookies["not-a-refresh-token-cookies"];

        if (token is null)
        {
            throw new Exception("Incorrect refresh token cookies");
        }

        var refreshToken = _unitOfWork.RefreshTokenRepository.Get(Guid.Parse(token)).Result;

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
        httpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
    }
    
    public async Task<string> Refresh(HttpContext httpContext)
    {
        var refreshToken = httpContext.Request.Cookies["not-a-refresh-token-cookies"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException("Refresh token is missing.");
        }
        
        var token = _unitOfWork.RefreshTokenRepository.Get(Guid.Parse(refreshToken)).Result;
        
        if (token is null)
        {
            throw new UnauthorizedAccessException("This refresh token is missing.");
        }
        
        if (token.ExpiryDate <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token has expired.");
        }
        
        var user = await _unitOfWork.UserRepository.Get(token.UserId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        var newAccessToken = _jwtProvider.GenerateAccessToken(user);
        
        if (newAccessToken is null)
        {
            throw new Exception("Failed to refresh token.");
        }
        
        return newAccessToken;
    }
}
