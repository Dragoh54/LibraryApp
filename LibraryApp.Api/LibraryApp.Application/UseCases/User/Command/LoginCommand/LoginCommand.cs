using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.LoginCommand;

public record LoginCommand : IRequest<(string, string)>
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public LoginCommand()
    {
    }

    public LoginCommand(string nickname, string email, string password)
    {
        Email = email;
        Password = password;
    }
}