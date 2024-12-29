using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.DataAccess.Extensions;

public static class RepositoryServices
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}