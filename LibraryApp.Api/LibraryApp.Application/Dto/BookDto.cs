using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Dto;

public record BookDto
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre {  get; set; } = string.Empty;
    public DateTime? TakenAt { get; set; } = null;
    public DateTime? ReturnBy { get; set; } = null;

    public Guid AuthorId { get; set; }

    public BookDto() { }
    public BookDto(Guid id, string isbn, string title, string description, string genre, DateTime? takenAt, DateTime? returnBy, Guid authorId)
    {
        Id = id;
        ISBN = isbn;
        Title = title;
        Description = description;
        Genre = genre;
        TakenAt = takenAt;
        ReturnBy = returnBy;
        AuthorId= authorId;
    }
}
