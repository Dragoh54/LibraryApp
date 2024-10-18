using LibraryApp.DomainModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<LibraryAppDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(LibraryAppDbContext)));
    });

var app = builder.Build();

app.Run();
