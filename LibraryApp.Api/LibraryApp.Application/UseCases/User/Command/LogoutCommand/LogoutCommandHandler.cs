using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel.Exceptions;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.LogoutCommand;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    
    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = request.Token;

        if (token is null)
        {
            throw new UnauthorizedAccessException("Incorrect refresh token");
        }

        var refreshToken = _unitOfWork.RefreshTokenRepository.Get(Guid.Parse(token)).Result;
        cancellationToken.ThrowIfCancellationRequested();

        if (refreshToken is null)
        {
            throw new NotFoundException("Refresh token not found");
        }

        refreshToken.IsUsed = true;
        refreshToken.WhenUsed = DateTime.UtcNow;

        await _unitOfWork.RefreshTokenRepository.Update(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}