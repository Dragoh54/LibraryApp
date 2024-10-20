using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Entities.Models;

public class BookEntity
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
    public DateTime TakenAt { get; set; }
    public DateTime ReturnBy { get; set; }

    public AuthorEntity Author { get; set; }
    public Guid AuthorId { get; set; }

    public UserEntity? User { get; set; }
    public Guid? UserId { get; set; } 

    public BookEntity() { }

    public BookEntity(Guid id, string iSBN, string title, string genre, string description, DateTime taken, DateTime expire, AuthorEntity author, UserEntity user)
    {
        Id = id;
        ISBN = iSBN;
        Title = title;
        Genre = genre;
        Description = description;
        TakenAt = taken;
        ReturnBy = expire;
        Author = author;
        AuthorId = author.Id;
        User = user;
        UserId = user.Id;
    }

    public BookEntity(Guid id, string iSBN, string title, string genre, string description, AuthorEntity author)
    {
        Id = id;
        ISBN = iSBN;
        Title = title;
        Genre = genre;
        Description = description;
        Author = author;
        AuthorId = author.Id;
    }
}
