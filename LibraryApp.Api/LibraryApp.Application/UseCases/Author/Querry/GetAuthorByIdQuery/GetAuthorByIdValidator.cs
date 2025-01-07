using FluentValidation;

namespace LibraryApp.Application.UseCases.Author.Querry.GetAuthorByIdQuerry;

public class GetAuthorByIdValidator : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdValidator()
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