using LibraryApp.Application.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories;

public class AuthorRepository : IRepository<AuthorEntity>
{
    private readonly LibraryAppDbContext _dbContext;

    public AuthorRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<AuthorEntity> Create(AuthorEntity item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(AuthorEntity item)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<AuthorEntity?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuthorEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AuthorEntity?> Update(AuthorEntity item)
    {
        throw new NotImplementedException();
    }
}
