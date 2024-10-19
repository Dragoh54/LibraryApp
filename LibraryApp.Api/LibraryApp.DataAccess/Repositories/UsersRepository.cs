﻿using LibraryApp.Application.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class UsersRepository : IRepository<UserEntity>
{
    private readonly LibraryAppDbContext _dbContext;

    public UsersRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserEntity> Create(UserEntity item)
    {
        var result = await _dbContext.Users.AddAsync(item);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<UserEntity>? Update(UserEntity item)
    {
        var result = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == item.Id);

        if(result is null)
        {
            return null;
        }

        result.Id = item.Id;
        result.Role = item.Role;
        result.Email = item.Email;
        result.Books = item.Books;
        result.Nickname = item.Nickname;
        result.PasswordHash = item.PasswordHash;

        await _dbContext.SaveChangesAsync();

        return result;
    }

    public async Task Delete(UserEntity item)
    {
        await _dbContext.Users
            .Where(u => u.Id == item.Id)
            .ExecuteDeleteAsync();
    }
    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<UserEntity> Get(Guid id)
    {
        throw new NotImplementedException();
    }


    public Task Save()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
