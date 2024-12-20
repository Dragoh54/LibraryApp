using LibraryApp.Application.Validators.UserValidators;

namespace LibraryApp.Application.UseCases.User.Command.LoginCommand;

public class LoginCommandValidator : RegisterLoginDtoValidator<LoginCommand>
{
}