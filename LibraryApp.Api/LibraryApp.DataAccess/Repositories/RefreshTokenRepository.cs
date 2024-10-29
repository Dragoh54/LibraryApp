using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DataAccess.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly LibraryAppDbContext _dbContext;

    public RefreshTokenRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<RefreshToken>> GetAll()
    {
        return await _dbContext.Tokens
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RefreshToken?> Get(Guid id)
    {
        return await _dbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Id == id);
    }

    public async Task<RefreshToken> Add(RefreshToken item)
    {
        var result = await _dbContext.Tokens.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<RefreshToken?> Update(RefreshToken item)
    {
        var existingToken = await _dbContext.Tokens
            .FirstOrDefaultAsync(rt => rt.Id == item.Id);

        if (existingToken is null)
        {
            return null;
        }

        existingToken.UserId = item.UserId;
        existingToken.ExpiryDate = item.ExpiryDate;
        existingToken.WhenUsed = item.WhenUsed;
        existingToken.IsUsed = item.IsUsed;

        await _dbContext.SaveChangesAsync();
        return existingToken;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(RefreshToken item)
    {
        _dbContext.Tokens.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByUserId(Guid userId)
    {
        return await _dbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.UserId == userId);
    }
}