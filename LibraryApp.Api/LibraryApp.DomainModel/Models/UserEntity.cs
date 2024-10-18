using LibraryApp.DomainModel.Enums;
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
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; }

    public List<BookEntity> Books { get; set; } = new List<BookEntity>();

    public UserEntity() { }

    public UserEntity(Guid id, string nickname, string email, string passwordHash, Role role, List<BookEntity> books)
    {
        Id = id;
        Nickname = nickname;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Books = books;
    }
}
