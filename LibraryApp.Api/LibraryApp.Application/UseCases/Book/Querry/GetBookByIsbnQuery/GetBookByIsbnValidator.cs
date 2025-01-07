using FluentValidation;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIsbnQuery;

public class GetBookByIsbnValidator : AbstractValidator<GetBookByIsbnQuery>
{
    public GetBookByIsbnValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .NotNull()
            .WithMessage("GetBookByIsbn is required and not empty");
        
        RuleFor(x => x.Isbn)
            .NotEmpty()
            .NotNull()
            .WithMessage("Isbn is required and not empty");
    }
}