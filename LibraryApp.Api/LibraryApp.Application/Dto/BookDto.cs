using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Dto;

public class BookDto
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre {  get; set; } = string.Empty;
    public DateTime? TakenAt { get; set; } = null;
    public DateTime? ReturnBy { get; set; } = null;

    public Guid AuthorId { get; set; }
    //public AuthorDto Author { get; set; }

    public BookDto() { }
    public BookDto(Guid id, string iSBN, string title, string description, string genre, DateTime? takenAt, DateTime? returnBy, Guid authorId)
    {
        Id = id;
        ISBN = iSBN;
        Title = title;
        Description = description;
        Genre = genre;
        TakenAt = takenAt;
        ReturnBy = returnBy;
        AuthorId= authorId;
    }

    public BookDto(string iSBN, string title, string description, string genre, DateTime? takenAt, DateTime? returnBy, AuthorDto author)
    {
        Id = Guid.NewGuid();
        ISBN = iSBN;
        Title = title;
        Description = description;
        Genre = genre;
        TakenAt = takenAt;
        ReturnBy = returnBy;
        //Author = author;
    }

    public BookDto(Guid id, string iSBN, string title, string description, string genre, AuthorDto author)
    {
        Id = id;
        ISBN = iSBN;
        Title = title;
        Description = description;
        Genre = genre;
        //Author = author;
    }

}
