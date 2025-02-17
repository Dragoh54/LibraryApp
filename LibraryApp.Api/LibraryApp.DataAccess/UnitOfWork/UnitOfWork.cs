﻿using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryAppDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private bool disposed = false;

    public IUserRepository UserRepository => _userRepository;
    public IAuthorRepository AuthorRepository => _authorRepository;
    public IBookRepository BookRepository => _bookRepository;
    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

    public UnitOfWork(LibraryAppDbContext dbContext, IUserRepository userRepository, IAuthorRepository authorRepository, IBookRepository bookRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed && disposing)
        {
            _dbContext.Dispose();
        }

        this.disposed = true;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
