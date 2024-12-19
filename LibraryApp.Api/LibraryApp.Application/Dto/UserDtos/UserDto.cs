using LibraryApp.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Dto;

public class UserDto
{
    public string Email {  get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public Role Role { get; set; }

    public UserDto() { }

    public UserDto(string email, string username, Role role)
    {
        Email = email;
        Nickname = username;
        Role = role;
    }
}
