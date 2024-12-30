using LibraryApp.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.UseCases.User.Command.LogoutCommand;

public record LogoutCommand : RefreshTokenDto, IRequest<bool>
{
    public string Token { get; set; }

    public LogoutCommand()
    {
    }

    public LogoutCommand(string token)
    {
        Token = token;
    }
}