using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetAllAuthorsQuery;

public record GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
{
    public GetAllAuthorsQuery()
    {
    }
}