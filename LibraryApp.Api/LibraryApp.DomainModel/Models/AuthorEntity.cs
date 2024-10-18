using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Entities.Models;

public class AuthorEntity
{
    public Guid Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

    public List<BookEntity> Books { get; set; } = new List<BookEntity>();

    public AuthorEntity() { }
    public AuthorEntity(Guid id, string surname, string country, DateTime birthDate, List<BookEntity> books)
    {
        Id = id;
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
        Books = books;
    }
}
