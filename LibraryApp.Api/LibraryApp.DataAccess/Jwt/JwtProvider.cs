using LibraryApp.Entities.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Jwt;

//public class JwtProvider(IOptions<JwtOptions> options)
//{
//    private readonly JwtOptions _options = options.Value;

//    public string GenerateToken(UserEntity user)
//    {
//        var signingCredentials = new SigningCredentials(
//                new SymmetricSecurityKey()
//                )

//        var token = new JwtSecurityToken(
//                signingCredentials: 
//            );
//    }
//}
