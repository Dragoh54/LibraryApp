﻿using System.Reflection;
using FluentValidation;
using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Interfaces.UnitOfWork;
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
using LibraryApp.DataAccess.Extensions;
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

        services.AddControllersWithViews();

        services.AddApiAuthenfication(Configuration);

        services.AddDbContext<LibraryAppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("LibraryAppDbContext")));
        
        services.AddMediatRServices();
        services.AddRepositories();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddJwt();
        
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
    }
}
