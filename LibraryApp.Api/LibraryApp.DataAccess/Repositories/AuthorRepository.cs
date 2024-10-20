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

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryAppDbContext _dbContext;

    public AuthorRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthorEntity> Create(AuthorEntity item)
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
        return await _dbContext.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<AuthorEntity>> GetAll()
    {
        return await _dbContext.Authors
            .AsNoTracking()
            .ToListAsync();
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

    public async Task<IEnumerable<BookEntity>?> GetAuthorBooks(AuthorEntity item)
    {
        var result = await _dbContext.Authors
            .Include(a => a.Books) 
            .AsNoTracking()        
            .FirstOrDefaultAsync(a => a.Id == item.Id);

        if (result is null)
        {
            return null;
        }

        return result.Books;
    }
}
