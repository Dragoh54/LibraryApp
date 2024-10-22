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
        var token = await _userService.Login(request.Email, request.Password);

        HttpContext.Response.Cookies.Append("tasty-cookies", token);

        return Results.Ok();
    }

    [HttpPost("/register")]
    public async Task<IResult> Register(RegisterUserRequest request)
    {
        await _userService.Register(request.Nickname, request.Email, request.Password);
        return Results.Ok();
    }

    [HttpGet("/logout")]
    public async Task<IResult> Logout()
    {
        throw new NotImplementedException();
    }

    [HttpGet("/getRole")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> GetRole()
    {
        return Results.Ok("Ok");
    }

    [HttpPost("/refresh")]
    public async Task<IResult> Refresh()
    {
        throw new NotImplementedException();
    }
}
