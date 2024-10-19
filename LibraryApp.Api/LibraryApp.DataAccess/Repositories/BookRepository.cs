using LibraryApp.Application.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class BookRepository : IRepository<BookEntity>
{
    private readonly LibraryAppDbContext _dbContext;

    public BookRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<BookEntity> Create(BookEntity item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(BookEntity item)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<BookEntity?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BookEntity?> Update(BookEntity item)
    {
        throw new NotImplementedException();
    }
}
