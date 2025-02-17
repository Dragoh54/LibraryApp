﻿using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Jwt;

public class JwtProvider(IConfiguration configuration, IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    private readonly string _secretKey = configuration["JWTSecretKey"];

    public string GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        Claim[] claims = [
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name,user.Nickname),
            new(ClaimTypes.Role,user.Role.ToString())
            ];

        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                SecurityAlgorithms.HmacSha256);
        
        cancellationToken.ThrowIfCancellationRequested();

        var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes)
            );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        cancellationToken.ThrowIfCancellationRequested();

        return tokenValue;
    }

    public RefreshToken GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token = new RefreshToken(Guid.NewGuid(), user.Id,
            DateTime.UtcNow.AddDays(_options.ExpiresDays));
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }
}
