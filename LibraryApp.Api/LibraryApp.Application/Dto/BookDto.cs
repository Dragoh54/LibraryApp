using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Dto;

public class BookDto
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre {  get; set; } = string.Empty;
    public DateTime? TakenAt { get; set; } = null;
    public DateTime? ReturnBy { get; set; } = null;

    public BookDto() { }
    public BookDto(string iSBN, string title, string description, string genre, DateTime? takenAt, DateTime? returnBy)
    {
        ISBN = iSBN;
        Title = title;
        Description = description;
        Genre = genre;
        TakenAt = takenAt;
        ReturnBy = returnBy;
    }

    public BookDto(string iSBN, string title, string description, string genre)
    {
        ISBN = iSBN;
        Title = title;
        Description = description;
        Genre = genre;
    }

}
