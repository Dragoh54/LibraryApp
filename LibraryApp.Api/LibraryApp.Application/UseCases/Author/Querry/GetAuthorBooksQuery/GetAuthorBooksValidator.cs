using FluentValidation;

namespace LibraryApp.Application.UseCases.Author.Querry.GetBooksByAuthorQuery;

public class GetAuthorBooksValidator : AbstractValidator<GetAuthorBooksQuery>
{
    public GetAuthorBooksValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Id model is required");
        
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotNull()
            .WithMessage("Id is required.");
    }
}