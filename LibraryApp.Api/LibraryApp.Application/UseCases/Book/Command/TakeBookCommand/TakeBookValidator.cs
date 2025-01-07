using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Book.Command.TakeBookCommand;

public class TakeBookValidator : AbstractValidator<TakeBookCommand>
{
    public TakeBookValidator()
    {
        RuleFor(book => book)
            .NotNull()
            .WithMessage("The book object cannot be null.");
        
        RuleFor(book => book.UserClaimId)
            .NotNull()
            .WithMessage("The user claim id cannot be null.")
            .NotEmpty()
            .WithMessage("The user claim id cannot be empty.");

        RuleFor(book => book.Id)
            .NotNull()
            .WithMessage("The book id cannot be null.")
            .NotEmpty()
            .WithMessage("The book id cannot be empty.");
    }
}