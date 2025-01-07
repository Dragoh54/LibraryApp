using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetBooksByAuthorQuery;

public record GetAuthorBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public Guid Id { get; set; }
    
    public GetAuthorBooksQuery()
    {
    }

    public GetAuthorBooksQuery(Guid authorId)
    {
        Id = authorId;
    }
}