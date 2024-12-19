using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetBooksByAuthorQuery;

public record GetAuthorBooksQuery : IdModel, IRequest<IEnumerable<BookDto>>
{
    public GetAuthorBooksQuery()
    {
    }

    public GetAuthorBooksQuery(Guid authorId)
    {
        Id = authorId;
    }
}