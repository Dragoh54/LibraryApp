using FluentValidation;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIdQuery;

public class GetBookByIdValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdValidator()
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