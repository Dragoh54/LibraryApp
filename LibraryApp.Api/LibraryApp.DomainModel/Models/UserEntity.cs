using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Entities.Models;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password {  get; set; } = string.Empty;
    public string Role {  get; set; } = string.Empty;

    public List<BookEntity> Books { get; set; } = new List<BookEntity>();
}
