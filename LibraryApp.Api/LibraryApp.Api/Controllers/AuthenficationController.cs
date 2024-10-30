using Azure.Core;
using LibraryApp.Api.Filters;
using LibraryApp.Application.Services;
using LibraryApp.Application.User;
using LibraryApp.DataAccess.Jwt;
using LibraryApp.DomainModel.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenficationController : Controller
{
    private readonly UserService _userService;
    private readonly JwtOptions _jwtOptions;

    public AuthenficationController(UserService userService, IOptions<JwtOptions> jwtOptions)
    {
        _userService = userService;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("/login")]
    [ServiceFilter(typeof(AllowAnonymousOnlyFilter))]
    public async Task<IResult> Login(LoginUserRequest request)
    {
        var (token, refreshToken) = await _userService.Login(request.Email, request.Password);
        
        HttpContext.Response.Cookies.Append("tasty-cookies", token, new CookieOptions()
        {
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(_jwtOptions.ExpiresMinutes)
        });
        HttpContext.Response.Cookies.Append("not-a-refresh-token-cookies", refreshToken, new CookieOptions()
        {
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromDays(_jwtOptions.ExpiresDays)
        });

        return Results.Ok();
    }

    [HttpPost("/register")]
    public async Task<IResult> Register(RegisterUserRequest request)
    {
        await _userService.Register(request.Nickname, request.Email, request.Password);
        return Results.Ok();
    }

    [HttpPost("/logout")]
    [Authorize]
    public async Task<IResult> Logout()
    {
        await _userService.Logout(HttpContext);
        return Results.Ok();
    }

    [HttpPost("/refresh")]
    [AllowAnonymous]
    public async Task<IResult> Refresh()
    {
        var token = await _userService.Refresh(HttpContext);
        
        HttpContext.Response.Cookies.Append("tasty-cookies", token, new CookieOptions()
        {
            Domain = "localhost",
            Secure = true,
            HttpOnly = true,
            MaxAge = TimeSpan.FromMinutes(_jwtOptions.ExpiresMinutes)
        });
        
        return Results.Ok(token);
    }
}
