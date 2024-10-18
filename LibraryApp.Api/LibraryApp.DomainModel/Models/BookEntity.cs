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
    public DateTime IssueDate { get; set; }
    public DateTime ExpireDate { get; set; }

    public AuthorEntity Author { get; set; }
    public Guid AuthorId { get; set; }
}
