using LibraryApp.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.UseCases.User.Command.RefreshCommand;

public record RefreshCommand : IRequest<string>
{
    public string Token { get; set; }

    public RefreshCommand()
    {
    }

    public RefreshCommand(string token)
    {
        Token = token;
    }
}