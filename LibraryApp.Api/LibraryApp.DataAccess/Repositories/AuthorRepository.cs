using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryAppDbContext _dbContext;

    public AuthorRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthorEntity> Add(AuthorEntity item)
    {
        var result = await _dbContext.Authors.AddAsync(item);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task Delete(AuthorEntity item)
    {
        await _dbContext.Authors
            .Where(a => a.Id == item.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<AuthorEntity?> Get(Guid id)
    {
        var author = await _dbContext.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return author;
    }

    public async Task<IEnumerable<AuthorEntity>> GetAll()
    {
        var authors = await _dbContext.Authors
            .AsNoTracking()
            .ToListAsync();

        return authors;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AuthorEntity?> Update(AuthorEntity item)
    {
        var result = await _dbContext.Authors
            .FirstOrDefaultAsync(a => a.Id == item.Id);

        if(result is null)
        {
            return null;
        }

        result.Id = item.Id;
        result.Surname = item.Surname;
        result.Country = item.Country;
        result.BirthDate = item.BirthDate;
        result.Books = item.Books;

        await _dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<IEnumerable<BookEntity>?> GetAuthorBooks(Guid id)
    {
        var result = await _dbContext.Authors
        .AsNoTracking()
        .Include(a => a.Books)
        .FirstOrDefaultAsync(a => a.Id == id);

        if (result is null)
        {
            throw new Exception("Author with this id doesn't exist");
        }

        return result.Books;
    }

    public async Task<PaginatedPagedResult<AuthorEntity>?> GetAuthors(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and pageSize must be greater than zero.");
        }

        var totalCount = await _dbContext.Authors.CountAsync();
        var items = await _dbContext.Authors
            .AsNoTracking()
            .OrderBy(a => a.Surname)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedPagedResult<AuthorEntity>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}
