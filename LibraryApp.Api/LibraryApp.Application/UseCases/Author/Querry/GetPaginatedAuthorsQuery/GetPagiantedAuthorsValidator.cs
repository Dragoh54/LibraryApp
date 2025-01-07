using FluentValidation;

namespace LibraryApp.Application.UseCases.Author.Querry.GetPaginatedAuthorsQuery;

public class GetPagiantedAuthorsValidator : AbstractValidator<GetPaginatedAuthorsQuery> 
{
    public GetPagiantedAuthorsValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("GetPaginatedAuthorsQuerry is required");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithMessage("page number is required")
            .GreaterThan(0)
            .WithMessage("Page must be greater than zero.");
        
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithMessage("page size is required")
            .GreaterThan(0)
            .WithMessage("Page size must be greater than zero.");
    }
}