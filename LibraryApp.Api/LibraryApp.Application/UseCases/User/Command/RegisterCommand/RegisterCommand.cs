using LibraryApp.Application.User;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.RegisterCommand;

public record RegisterCommand : RegisterLoginUserDto, IRequest<UserDto>
{
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