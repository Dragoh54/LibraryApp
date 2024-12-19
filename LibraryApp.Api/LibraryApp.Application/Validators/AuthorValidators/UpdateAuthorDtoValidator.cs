using FluentValidation;
using LibraryApp.Application.Dto;

namespace LibraryApp.Application.Validators;

public class UpdateAuthorDtoValidator<T> : AbstractValidator<T> where T : UpdateAuthorDto
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(author => author)
            .NotNull()
            .WithMessage("Author object cannot be null.");

        RuleFor(author => author.Id)
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