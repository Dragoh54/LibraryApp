﻿using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Dto;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public List<BookDto> Books { get; set; } = new List<BookDto>();

    public AuthorDto() { }
    public AuthorDto(Guid id, string surname, string country, DateTime birthDate)
    {
        Id = id;
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
    }
}
