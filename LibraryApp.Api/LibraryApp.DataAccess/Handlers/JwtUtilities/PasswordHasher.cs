using LibraryApp.Application.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Utilities;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password, CancellationToken cancellationToken)
    {
        var token = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        
        cancellationToken.ThrowIfCancellationRequested();
        return token;
    }

    public bool Verify(string password, string hashedPassword, CancellationToken cancellationToken)
    {
        var success = BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        
        cancellationToken.ThrowIfCancellationRequested();
        return success;
    }
}
