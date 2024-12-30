using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.Auth;

public interface IPasswordHasher
{
    string Generate(string password, CancellationToken cancellationToken);
    bool Verify(string password, string hashedPassword, CancellationToken cancellationToken);
}
