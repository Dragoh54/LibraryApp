using FluentValidation;
using LibraryApp.DataAccess.Dto;

namespace LibraryApp.Application.Validators;

public class TakeBookValidator<T> : AbstractValidator<T> where T : TakeBookDto 
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
            .WithMessage("The book object cannot be null.");
    }
}