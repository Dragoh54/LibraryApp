using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.DeleteAuthorCommand;

public record DeleteAuthorCommand :  IRequest<AuthorDto>
{
    public Guid Id { get; set; }
    
    public DeleteAuthorCommand()
    {
    }

    public DeleteAuthorCommand(Guid id)
    {
        Id = id;
    }
}