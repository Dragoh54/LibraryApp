using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;

public class AddAuthorValidator : AbstractValidator<AddAuthorCommand> 
{
    public AddAuthorValidator()
    {
        RuleFor(author => author)
            .NotNull()
            .WithMessage("Author object cannot be null.");

        RuleFor(author => author.Surname)
            .NotEmpty()
            .WithMessage("Surname can't be empty.")
            .Must(s => !string.IsNullOrWhiteSpace(s))
            .WithMessage("Surname cannot contain only whitespace.");

        RuleFor(author => author.Country)
            .NotEmpty()
            .WithMessage("Country can't be empty.")
            .Must(c => !string.IsNullOrWhiteSpace(c))
            .WithMessage("Country cannot contain only whitespace.");

        RuleFor(author => author.BirthDate)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Invalid date of birth.");
    }
}