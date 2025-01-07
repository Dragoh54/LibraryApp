using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetAuthorByIdQuerry;

public record GetAuthorByIdQuery : IRequest<AuthorDto>
{
    public Guid Id { get; set; }
    public GetAuthorByIdQuery() { }
    public GetAuthorByIdQuery(Guid id)
    {
        Id = id;
    }
}