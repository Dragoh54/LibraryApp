using LibraryApp.Application.Dto;
using LibraryApp.Application.Filters;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetPaginatedAuthorsQuery;

public record GetPaginatedAuthorsQuery : PaginatedResultDto<AuthorFilters>, IRequest<PaginatedPagedResult<AuthorDto>>
{
    public GetPaginatedAuthorsQuery()
    {
    }

    public GetPaginatedAuthorsQuery(AuthorFilters filters, int pageNumber, int pageSize)
    : base(filters, pageNumber, pageSize)
    {
    }
}