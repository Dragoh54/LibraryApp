using LibraryApp.Application.Dto;
using LibraryApp.Application.Filters;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetPaginatedBooksQuery;

public record GetPaginatedBooksQuery : PaginatedResultDto<BookFilters>, IRequest<PaginatedPagedResult<BookDto>>
{
    public GetPaginatedBooksQuery()
    {
    }

    public GetPaginatedBooksQuery(BookFilters filters, int pageNumber, int pageSize)
        : base(filters, pageNumber, pageSize)
    {
    }
}