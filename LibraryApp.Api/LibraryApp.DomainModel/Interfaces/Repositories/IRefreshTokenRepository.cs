using LibraryApp.Entities.Models;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
    public Task<RefreshToken?> GetByUserId(Guid userId, CancellationToken cancellationToken);
}