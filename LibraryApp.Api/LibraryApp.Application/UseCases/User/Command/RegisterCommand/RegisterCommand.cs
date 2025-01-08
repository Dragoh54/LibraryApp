using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.RegisterCommand;

public record RegisterCommand : IRequest<UserDto>
{
    public string Nickname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public RegisterCommand()
    {
    }

    public RegisterCommand(string nickname, string email, string password)
    {
        Nickname = nickname;
        Email = email;
        Password = password;
    }
    
}