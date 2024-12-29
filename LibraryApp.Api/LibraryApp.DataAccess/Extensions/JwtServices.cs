using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.DataAccess.Jwt;
using LibraryApp.DataAccess.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.DataAccess.Extensions;

public static class JwtServices
{
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}