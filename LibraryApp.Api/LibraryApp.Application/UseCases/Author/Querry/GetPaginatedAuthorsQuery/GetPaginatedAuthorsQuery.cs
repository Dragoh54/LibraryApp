using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetPaginatedAuthorsQuery;

public record GetPaginatedAuthorsQuery : IRequest<PaginatedPagedResult<AuthorDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public GetPaginatedAuthorsQuery()
    {
    }

    public GetPaginatedAuthorsQuery(int pageNumber = 1, int pageSize = 10)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}