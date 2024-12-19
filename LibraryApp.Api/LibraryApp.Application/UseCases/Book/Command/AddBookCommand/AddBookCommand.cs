using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.AddBookCommand;

public record AddBookCommand : CreateBookDto, IRequest<BookDto>
{
    public AddBookCommand()
    {
    }

    public AddBookCommand(string isbn, string title, string description, string genre, Guid authorId) : base(isbn,
        title, description, genre, authorId)
    {
    }
}