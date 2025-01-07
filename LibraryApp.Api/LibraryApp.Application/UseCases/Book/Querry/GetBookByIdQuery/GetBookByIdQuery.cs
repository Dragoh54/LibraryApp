using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIdQuery;

public record GetBookByIdQuery : IRequest<BookDto>
{
    public Guid Id { get; set; }
    
    public GetBookByIdQuery() { }
    public GetBookByIdQuery(Guid id)
    {
        Id = id;
    }
}