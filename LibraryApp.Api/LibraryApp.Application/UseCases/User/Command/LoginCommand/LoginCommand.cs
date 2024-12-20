using LibraryApp.Application.User;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.LoginCommand;

public record LoginCommand : RegisterLoginUserDto, IRequest<(string, string)>
{
    public LoginCommand()
    {
    }

    public LoginCommand(string nickname, string email, string password)
    {
        Nickname = nickname;
        Email = email;
        Password = password;
    }
}