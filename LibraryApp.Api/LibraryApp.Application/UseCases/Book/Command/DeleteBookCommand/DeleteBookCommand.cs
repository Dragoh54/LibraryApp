using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.DeleteBookCommand;

public record DeleteBookCommand : IRequest<BookDto>
{
    public Guid Id { get; set; }
    
    public DeleteBookCommand()
    {
    }

    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }
}