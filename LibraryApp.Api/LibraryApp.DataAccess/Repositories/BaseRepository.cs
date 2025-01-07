using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DataAccess.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : IdEntity
{
    protected readonly LibraryAppDbContext _dbContext;
    protected DbSet<T> _dbSet;
    

    public BaseRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        var res = await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        return res;
    }

    public async Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entity;
    }

    public async Task<T> Add(T item, CancellationToken cancellationToken)
    {
        var result = await _dbSet.AddAsync(item, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        return result.Entity;
    }

    public async Task<T?> Update(T item, CancellationToken cancellationToken)
    {
        _dbSet.Update(item);
        cancellationToken.ThrowIfCancellationRequested();
        
        return item;
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
    }

    public async Task Delete(T item, CancellationToken cancellationToken)
    {
        _dbSet.Remove(item);
        cancellationToken.ThrowIfCancellationRequested();
    }
}