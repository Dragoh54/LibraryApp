using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DataAccess.Dto;

namespace LibraryApp.DataAccess.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryAppDbContext _dbContext;

    public BookRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookEntity> Add(BookEntity item)
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

    public async Task<PaginatedPagedResult<BookEntity>?> GetBooks(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and pageSize must be greater than zero.");
        }

        var totalCount = await _dbContext.Books.CountAsync();
        var items = await _dbContext.Books
            .AsNoTracking()
            .OrderBy(b => b.Title) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedPagedResult<BookEntity>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
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
        result.TakenAt = item.TakenAt;
        result.ReturnBy = item.ReturnBy;
        result.Author = item.Author;
        result.AuthorId = item.AuthorId;
        result.User = item.User;
        result.UserId = item.UserId;

        await _dbContext.SaveChangesAsync();

        return result;
    }
}
