using LibraryApp.Application.Dto;
using LibraryApp.Application.Filters;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetPaginatedAuthorsQuery;

public record GetPaginatedAuthorsQuery : IRequest<PaginatedPagedResult<AuthorDto>>
{
    public AuthorFilters Filters { get; set; } = new AuthorFilters();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public GetPaginatedAuthorsQuery()
    {
    }

    public GetPaginatedAuthorsQuery(AuthorFilters filters, int pageNumber = 1, int pageSize = 10)
    {
        Filters = filters;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}