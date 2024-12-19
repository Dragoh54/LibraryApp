using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.UpdateBookCommand;

public record UpdateBookCommand : UpdateBookDto, IRequest<BookDto>
{
    public UpdateBookCommand()
    {
    }

    public UpdateBookCommand(Guid id, string isbn, string title, string description, string genre, Guid authorId)
        : base(id, isbn, title, description, genre, authorId)
    {
    }
}