using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetAllBooksQuery;

public record GetAllBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public GetAllBooksQuery()
    {
    }   
}