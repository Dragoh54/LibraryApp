using LibraryApp.Entities.Models;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    public Task<RefreshToken?> GetByUserId(Guid userId);
}