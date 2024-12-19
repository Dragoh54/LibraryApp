using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.User;

public record RegisterUserDto(
    [Required] string Nickname,
    [Required] string Password,
    [Required] string Email
    );
