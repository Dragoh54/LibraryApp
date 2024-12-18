using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.DeleteAuthorCommand;

public record DeleteAuthorCommand : IdModel, IRequest<AuthorDto>
{
    public DeleteAuthorCommand()
    {
    }

    public DeleteAuthorCommand(Guid id)
    {
        Id = id;
    }
}