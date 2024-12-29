using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Jwt;

public class JwtOptions
{
    public int ExpiresMinutes { get; set; }
    public int ExpiresDays { get; set; }
}
