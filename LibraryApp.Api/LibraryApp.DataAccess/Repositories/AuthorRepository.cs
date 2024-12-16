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

public class AuthorRepository : BaseRepository<AuthorEntity>, IAuthorRepository
{
    public AuthorRepository(LibraryAppDbContext dbContext) : base(dbContext)
    {
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
