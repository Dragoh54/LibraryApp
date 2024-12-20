﻿using LibraryApp.DataAccess.Configurations;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        new AuthorConfiguration().Configure(modelBuilder.Entity<AuthorEntity>());
        new BookConfiguration().Configure(modelBuilder.Entity<BookEntity>());
        new UserConfiguration().Configure(modelBuilder.Entity<UserEntity>());
        new RefreshTokenConfiguration().Configure(modelBuilder.Entity<RefreshToken>());

        base.OnModelCreating(modelBuilder);
    }
}
