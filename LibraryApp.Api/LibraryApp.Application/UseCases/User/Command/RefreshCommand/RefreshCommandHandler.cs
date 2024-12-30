using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.RefreshCommand;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public RefreshCommandHandler(IUnitOfWork appUnitOfWork, IJwtProvider jwtProvider)
    {
        _unitOfWork = appUnitOfWork;
        _jwtProvider = jwtProvider;
    }


    public async Task<string> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.Token;
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException("Refresh token is missing.");
        }
        
        var token = await _unitOfWork.RefreshTokenRepository.Get(Guid.Parse(refreshToken), cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (token is null)
        {
            throw new UnauthorizedAccessException("This refresh token is missing.");
        }
        
        if (token.ExpiryDate <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token has expired.");
        }
        
        var user = await _unitOfWork.UserRepository.Get(token.UserId, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        var newAccessToken = _jwtProvider.GenerateAccessToken(user, cancellationToken);
        
        if (newAccessToken is null)
        {
            throw new Exception("Failed to refresh token.");
        }
        
        cancellationToken.ThrowIfCancellationRequested();
        return newAccessToken;
    }
}