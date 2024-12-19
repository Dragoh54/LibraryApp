using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIdQuery;

public record GetBookByIdQuery : IdModel, IRequest<BookDto>
{
    public GetBookByIdQuery() { }
    public GetBookByIdQuery(Guid id)
    {
        Id = id;
    }
}