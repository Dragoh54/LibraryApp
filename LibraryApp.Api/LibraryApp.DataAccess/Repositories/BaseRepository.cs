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
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> Get(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<T> Add(T item)
    {
        var result = await _dbSet.AddAsync(item);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<T?> Update(T item)
    {
        _dbSet.Update(item);
        return item;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T item)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == item.Id);
        
        _dbSet.Remove(entity);
    }
}