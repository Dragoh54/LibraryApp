using LibraryApp.DomainModel;
using LibraryApp.DomainModel.Enums;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.DataSeeder;

public class DataSeeder
{
    public readonly LibraryAppDbContext _dbContext;

    public DataSeeder(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        List<AuthorEntity> authors = new List<AuthorEntity>
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

        List<UserEntity> users = new List<UserEntity>
        {
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "booklover123",
                Email = "booklover123@example.com",
                PasswordHash = "hashedpassword1",
                Role = Role.User
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "adminuser",
                Email = "admin@example.com",
                PasswordHash = "hashedpassword2",
                Role = Role.Admin
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "literaturefan",
                Email = "fan@example.com",
                PasswordHash = "hashedpassword3",
                Role = Role.User
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "historybuff",
                Email = "historybuff@example.com",
                PasswordHash = "hashedpassword4",
                Role = Role.User
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "sciencegeek",
                Email = "sciencegeek@example.com",
                PasswordHash = "hashedpassword5",
                Role = Role.User
            }
        };

        List<BookEntity> books = new List<BookEntity>
        {
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0261103573",
                Title = "The Hobbit",
                Genre = "Fantasy",
                Description = "A fantasy novel by J.R.R. Tolkien.",
                Author = authors[0],
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
                Author = authors[1],
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
                Author = authors[2],
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
                Author = authors[3],
                AuthorId = authors[3].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0141439518",
                Title = "Pride and Prejudice",
                Genre = "Classic",
                Description = "A classic novel by Jane Austen.",
                Author = authors[4],
                AuthorId = authors[4].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0261102385",
                Title = "The Lord of the Rings",
                Genre = "Fantasy",
                Description = "An epic high-fantasy novel by J.R.R. Tolkien.",
                Author = authors[0],
                AuthorId = authors[0].Id
            },
            new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-0747581086",
                Title = "Harry Potter and the Half-Blood Prince",
                Genre = "Fantasy",
                Description = "The sixth book in the Harry Potter series by J.K. Rowling.",
                Author = authors[1],
                AuthorId = authors[1].Id
            }
        };

        if (!_dbContext.Authors.Any())
        {
            _dbContext.Authors.AddRange(authors);
        }

        if (!_dbContext.Users.Any())
        {
            _dbContext.Users.AddRange(users);
        }

        if (!_dbContext.Books.Any())
        {
            _dbContext.Books.AddRange(books);
        }

        _dbContext.SaveChanges();
    }
}

