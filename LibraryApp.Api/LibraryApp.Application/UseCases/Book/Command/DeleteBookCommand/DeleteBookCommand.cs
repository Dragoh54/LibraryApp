using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.DeleteBookCommand;

public record DeleteBookCommand : IdModel, IRequest<BookDto>
{
    public DeleteBookCommand()
    {
    }

    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }
}