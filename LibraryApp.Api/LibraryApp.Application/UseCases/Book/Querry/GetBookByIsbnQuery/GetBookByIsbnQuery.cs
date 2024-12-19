using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIsbnQuery;

public record GetBookByIsbnQuery : IRequest<BookDto>
{
    public string Isbn { get; set; } = string.Empty;

    public GetBookByIsbnQuery()
    {
    }

    public GetBookByIsbnQuery(string isbn)
    {
        Isbn = isbn;
    }
}