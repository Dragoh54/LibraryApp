using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.UpdateBookCommand;

public record UpdateBookCommand : IRequest<BookDto>
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre {  get; set; } = string.Empty;

    public Guid AuthorId { get; set; }
    
    public UpdateBookCommand() {}

    public UpdateBookCommand(Guid id, string isbn, string title, string description, string genre, Guid authorId)
    {
        Id = id;
        ISBN = isbn;
        Title = title;
        Description = description;
        Genre = genre;
        AuthorId = authorId;
    }
}