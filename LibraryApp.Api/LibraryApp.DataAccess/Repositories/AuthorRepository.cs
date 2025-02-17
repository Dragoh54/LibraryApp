﻿using LibraryApp.Application.Interfaces.Repositories;
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
using LibraryApp.Application.Filters;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApp.DataAccess.Repositories;

public class AuthorRepository : BaseRepository<AuthorEntity>, IAuthorRepository
{
    public AuthorRepository(LibraryAppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<BookEntity>?> GetAuthorBooks(Guid id, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Authors
        .AsNoTracking()
        .Include(a => a.Books)
        .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return result?.Books;
    }
    
    public async Task<(List<AuthorEntity>?, int)> GetAuthors(AuthorFilters filter, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Authors
            .AsNoTracking()
            .Where(a => filter.Surname.IsNullOrEmpty() || a.Surname.Contains(filter.Surname))
            .Where(a => a.Country.Contains(filter.Country))
            .Where(a => filter.DateOfBirth.IsNullOrEmpty() || a.BirthDate.Date == DateTime.Parse(filter.DateOfBirth).Date)
            .AsQueryable();
        
        cancellationToken.ThrowIfCancellationRequested();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(a => a.Surname) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return (items, totalCount);
    }
}
