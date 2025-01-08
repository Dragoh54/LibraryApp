using Azure.Core;
using FluentValidation;
using LibraryApp.Api.Filters;
using LibraryApp.Application.UseCases.User.Command.LoginCommand;
using LibraryApp.Application.UseCases.User.Command.LogoutCommand;
using LibraryApp.Application.UseCases.User.Command.RefreshCommand;
using LibraryApp.Application.UseCases.User.Command.RegisterCommand;
using LibraryApp.DataAccess.Jwt;
using LibraryApp.DomainModel.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LogoutCommand = LibraryApp.Application.UseCases.User.Command.LogoutCommand.LogoutCommand;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenficationController : Controller
{
    private readonly JwtOptions _jwtOptions;
    private readonly IMediator _mediator;

    public AuthenficationController(IMediator mediator, IOptions<JwtOptions> jwtOptions)
    {
        _mediator = mediator;
        _jwtOptions = jwtOptions.Value;
    }
    
    [HttpPost("/login")]
    [ServiceFilter(typeof(AllowAnonymousOnlyFilter))]
    public async Task<IResult> Login(LoginCommand user, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await _mediator.Send(user, cancellationToken);
        
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
    public async Task<IResult> Register(RegisterCommand user, CancellationToken cancellationToken)
    {
        var resultUser = await _mediator.Send(user, cancellationToken);
        return Results.Ok(resultUser);
    }
    
    [HttpPost("/logout")]
    [Authorize]
    public async Task<IResult> Logout(CancellationToken cancellationToken)
    {
        string? refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var success = await _mediator.Send(new LogoutCommand(refreshToken), cancellationToken);
        
        HttpContext.Response.Cookies.Delete("tasty-cookies");
        HttpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
        
        return Results.Ok(success);
    }
    
    [HttpPost("/refresh")]
    [AllowAnonymous]
    public async Task<IResult> Refresh(CancellationToken cancellationToken)
    {
        string? refreshToken = HttpContext.Request.Cookies["not-a-refresh-token-cookies"];
        var token = await _mediator.Send(new RefreshCommand(refreshToken), cancellationToken);
        
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
