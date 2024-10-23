using Azure.Core;
using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel.Enums;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}
