using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.User.Command.RefreshCommand;

public class RefreshCommandValidator : RefreshTokenValidator<RefreshCommand>
{
}