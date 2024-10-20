using LibraryApp.Application.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryAppDbContext _dbContext;

    public BookRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookEntity> Create(BookEntity item)
    {
        var result = await _dbContext.Books.AddAsync(item);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task Delete(BookEntity item)
    {
        await _dbContext.Books
            .Where(b => b.Id == item.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<BookEntity?> Get(Guid id)
    {
        return await _dbContext.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BookEntity?> GetByISBN(string ISBN)
    {
        return await _dbContext.Books
           .AsNoTracking()
           .FirstOrDefaultAsync(b => b.ISBN == ISBN);
    }

    public async Task<IEnumerable<BookEntity>> GetAll()
    {
        return await _dbContext.Books
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<BookEntity?> Update(BookEntity item)
    {
        var result = await _dbContext.Books
            .FirstOrDefaultAsync(b => b.Id == item.Id);

        if (result is null)
        {
            return null;
        }

        result.Id = item.Id;
        result.ISBN = item.ISBN;
        result.Title = item.Title;
        result.Genre = item.Genre;
        result.Description = item.Description;
        result.Taken = item.Taken;
        result.Expire = item.Expire;
        result.Author = item.Author;
        result.AuthorId = item.AuthorId;
        result.User = item.User;
        result.UserId = item.UserId;

        await _dbContext.SaveChangesAsync();

        return result;
    }
}
