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

    public List<BookEntity> Books { get; set; }
}
