using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.User.Command.LogoutCommand;

public class LogoutCommandValidator : RefreshTokenValidator<LogoutCommand>
{
}