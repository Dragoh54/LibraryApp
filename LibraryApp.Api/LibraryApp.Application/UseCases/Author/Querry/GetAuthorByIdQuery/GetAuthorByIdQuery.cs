using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetAuthorByIdQuerry;

public record GetAuthorByIdQuery : IdModel, IRequest<AuthorDto>
{
    public GetAuthorByIdQuery() { }
    public GetAuthorByIdQuery(Guid id)
    {
        Id = id;
    }
}