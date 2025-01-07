using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Application.Filters;
using LibraryApp.DataAccess.Dto;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.DataAccess.Repositories;

public class BookRepository : BaseRepository<BookEntity>, IBookRepository
{
    public BookRepository(LibraryAppDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<BookEntity?> GetByISBN(string ISBN, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
           .AsNoTracking()
           .FirstOrDefaultAsync(b => b.ISBN == ISBN, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return book;
    }
    
    public async Task<(List<BookEntity>?, int)> GetBooks(BookFilters filter, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .Where(b => filter.Title.IsNullOrEmpty() || b.Title.Contains(filter.Title))
            .Where(b => filter.Genre.IsNullOrEmpty() || b.Genre.Contains(filter.Genre))
            .Where(b => filter.Author.IsNullOrEmpty() || b.Author.Surname.Contains(filter.Author))
            .AsQueryable();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var totalCount = await query.CountAsync(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        var items = await query
            .OrderBy(b => b.Title) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return (items, totalCount);
    }
}
