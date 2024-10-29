using Azure.Core;
using LibraryApp.Application.Services;
using LibraryApp.Application.User;
using LibraryApp.DomainModel.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenficationController : Controller
{
    private readonly UserService _userService;

    public AuthenficationController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/login")]
    public async Task<IResult> Login(LoginUserRequest request)
    {
        var (token, refreshToken )= await _userService.Login(request.Email, request.Password);

        HttpContext.Response.Cookies.Append("tasty-cookies", token);
        HttpContext.Response.Cookies.Append("not-a-refresh-token-cookies", refreshToken);

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

    [HttpGet("/getRole")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetRole()
    {
        return Results.Ok("Ok");
    }

    [HttpPost("/refresh")]
    [Authorize]
    public async Task<IResult> Refresh()
    {
        return Results.Ok();
    }
}
