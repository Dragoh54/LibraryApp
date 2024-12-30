using LibraryApp.DataAccess.Utilities;
using LibraryApp.DomainModel;
using LibraryApp.DomainModel.Enums;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.DataSeeder;

public static class DataSeeder
{

    public static List<UserEntity> GenerateUsers()
    {
        PasswordHasher ph = new PasswordHasher();
        
        return new List<UserEntity>
        {
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "booklover123",
                Email = "booklover123@example.com",
                PasswordHash = ph.Generate("hashedpassword1", new CancellationToken()),
                Role = Role.User
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "adminuser",
                Email = "admin@example.com",
                PasswordHash = ph.Generate("hashedpassword2", new CancellationToken()),
                Role = Role.Admin
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "literaturefan",
                Email = "fan@example.com",
                PasswordHash = ph.Generate("hashedpassword3", new CancellationToken()),
                Role = Role.User
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "historybuff",
                Email = "historybuff@example.com",
                PasswordHash = ph.Generate("hashedpassword4", new CancellationToken()),
                Role = Role.User
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "sciencegeek",
                Email = "sciencegeek@example.com",
                PasswordHash = ph.Generate("hashedpassword5", new CancellationToken()),
                Role = Role.User
            }
        };
    }

    public static List<AuthorEntity> GenerateAuthors()
    {
        return new List<AuthorEntity>
        {
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Surname = "Tolkien",
                Country = "United Kingdom",
                BirthDate = DateTime.SpecifyKind(DateTime.Parse("1892-01-03"), DateTimeKind.Utc),
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Surname = "Rowling",
                Country = "United Kingdom",
                BirthDate = DateTime.SpecifyKind(DateTime.Parse("1965-07-31"), DateTimeKind.Utc),
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Surname = "Martin",
                Country = "United States",
                BirthDate = DateTime.SpecifyKind(DateTime.Parse("1948-09-20"), DateTimeKind.Utc),
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Surname = "Orwell",
                Country = "United Kingdom",
                BirthDate = DateTime.SpecifyKind(DateTime.Parse("1903-06-25"), DateTimeKind.Utc),
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Surname = "Austen",
                Country = "United Kingdom",
                BirthDate = DateTime.SpecifyKind(DateTime.Parse("1775-12-16"), DateTimeKind.Utc),
            }
        };
    }

    public static List<BookEntity> GenerateBooks(List<AuthorEntity> authors, List<UserEntity> users )
    {
        return new List<BookEntity>
        {
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0261103573",
                Title = "The Hobbit",
                Genre = "Fantasy",
                Description = "A fantasy novel by J.R.R. Tolkien.",
                AuthorId = authors[0].Id,
                TakenAt = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(-10), DateTimeKind.Utc),
                ReturnBy = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(20), DateTimeKind.Utc),
                UserId = users[0].Id 
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0747532743",
                Title = "Harry Potter and the Philosopher's Stone",
                Genre = "Fantasy",
                Description = "The first book in the Harry Potter series by J.K. Rowling.",
                AuthorId = authors[1].Id,
                TakenAt = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(-5), DateTimeKind.Utc),
                ReturnBy = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(15), DateTimeKind.Utc),
                UserId = users[0].Id 
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0553103540",
                Title = "A Game of Thrones",
                Genre = "Fantasy",
                Description = "The first book in the A Song of Ice and Fire series by George R.R. Martin.",
                AuthorId = authors[2].Id,
                TakenAt = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(-20), DateTimeKind.Utc),
                ReturnBy = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(5), DateTimeKind.Utc),
                UserId = users[2].Id 
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0451524935",
                Title = "1984",
                Genre = "Dystopian",
                Description = "A dystopian novel by George Orwell.",
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0141439518",
                Title = "Pride and Prejudice",
                Genre = "Classic",
                Description = "A classic novel by Jane Austen.",
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0261102385",
                Title = "The Lord of the Rings",
                Genre = "Fantasy",
                Description = "An epic high-fantasy novel by J.R.R. Tolkien.",
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0747581086",
                Title = "Harry Potter and the Half-Blood Prince",
                Genre = "Fantasy",
                Description = "The sixth book in the Harry Potter series by J.K. Rowling.",
                AuthorId = authors[1].Id
            }
        };
    }
}

