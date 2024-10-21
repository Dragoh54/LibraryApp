using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Dto;

public class AuthorDto
{
    public string Surname { get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public List<BookEntity> Books { get; set; } = new List<BookEntity>();
    
    public AuthorDto() { }
    public AuthorDto(string surname, string country, DateTime birthDate, List<BookEntity> books)
    {
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
        Books = books;
    }
}
