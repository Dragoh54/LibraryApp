using LibraryApp.DataAccess.Configurations;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DataAccess.DataSeeder;

namespace LibraryApp.DomainModel;

public class LibraryAppDbContext(DbContextOptions<LibraryAppDbContext> options)
    : DbContext(options)
{
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RefreshToken> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryAppDbContext).Assembly);
        
        List<UserEntity> users = DataSeeder.GenerateUsers();
        List<AuthorEntity> authors = DataSeeder.GenerateAuthors();
        List<BookEntity> books = DataSeeder.GenerateBooks(authors, users);
        
        modelBuilder.Entity<UserEntity>().HasData(users);
        modelBuilder.Entity<AuthorEntity>().HasData(authors);
        modelBuilder.Entity<BookEntity>().HasData(books);

        base.OnModelCreating(modelBuilder);
    }
}
