﻿using LibraryApp.Application.Interfaces.Auth;
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
using LibraryApp.Api.Middlewares;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.Api.Requirements;
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

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<AllowAnonymousOnlyFilter>();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        //app.UseExceptionHandler();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseMiddleware<TokenValidationMiddleware>();
        
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

        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryAppDbContext>();
            dbContext.Database.Migrate();
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            seeder.Seed();
        }
    }
}
