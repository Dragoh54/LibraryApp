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

public class BookRepository : BaseRepository<BookEntity>, IBookRepository
{
    public BookRepository(LibraryAppDbContext dbContext) : base(dbContext)
    {
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
}
