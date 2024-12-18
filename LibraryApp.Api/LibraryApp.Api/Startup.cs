using System.Reflection;
using FluentValidation;
using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.Application.Services;
using LibraryApp.DataAccess.DataSeeder;
using LibraryApp.DataAccess.Jwt;
using LibraryApp.DataAccess.Repositories;
using LibraryApp.DataAccess.UnitOfWork;
using LibraryApp.DataAccess.Utilities;
using LibraryApp.DomainModel;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Api;
using Microsoft.Extensions.Configuration;
using LibraryApp.Api.Extensions;
using LibraryApp.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.Api.Requirements;
using LibraryApp.Application.Extensions;
using LibraryApp.Application.Validators;
using LibraryApp.DataAccess.Dto;
using MediatR;
using Microsoft.Extensions.Options;

namespace LibraryApp.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<JwtOptions>(Configuration.GetSection(nameof(JwtOptions)));

        // services.AddControllersWithViews(options =>
        // {
        //     options.Filters.Add<ValidationFilter>();
        // });
        
        services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();
        services.AddScoped<IValidator<CreateAuthorDto>, CreateAuthorDtoValidator>();

        services.AddControllersWithViews();

        services.AddApiAuthenfication(Configuration);

        services.AddDbContext<LibraryAppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("LibraryAppDbContext")));
        services.AddTransient<DataSeeder>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<UserService>();
        services.AddScoped<BookService>();
        services.AddScoped<AuthorService>();
        
        services.AddMediatRServices();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<AllowAnonymousOnlyFilter>();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, string[] args)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
         
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always,
            
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        if (args.Contains("--seed"))
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LibraryAppDbContext>();
                dbContext.Database.Migrate();
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                seeder.Seed();
            }
        }
    }
}
