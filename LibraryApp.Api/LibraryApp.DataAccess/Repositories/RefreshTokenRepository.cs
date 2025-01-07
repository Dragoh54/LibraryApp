using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.DomainModel;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DataAccess.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(LibraryAppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RefreshToken?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var token = await _dbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.UserId == userId, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }
}