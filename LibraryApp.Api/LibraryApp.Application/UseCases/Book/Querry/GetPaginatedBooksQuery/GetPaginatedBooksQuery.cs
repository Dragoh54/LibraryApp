using LibraryApp.Application.Dto;
using LibraryApp.Application.Filters;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetPaginatedBooksQuery;

public record GetPaginatedBooksQuery : IRequest<PaginatedPagedResult<BookDto>>
{
    public BookFilters Filters { get; set; } = new BookFilters();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public GetPaginatedBooksQuery()
    {
    }

    public GetPaginatedBooksQuery(BookFilters filters, int pageNumber = 1, int pageSize = 10)
    {
        Filters = filters;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}