using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel.Exceptions;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.LogoutCommand;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public LogoutCommandHandler(IUnitOfWork appUnitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _unitOfWork = appUnitOfWork;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    
    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = request.HttpContext.Request.Cookies["not-a-refresh-token-cookies"];

        if (token is null)
        {
            throw new UnauthorizedAccessException("Incorrect refresh token cookies");
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
        
        request.HttpContext.Response.Cookies.Delete("tasty-cookies");
        request.HttpContext.Response.Cookies.Delete("not-a-refresh-token-cookies");
        
        cancellationToken.ThrowIfCancellationRequested();
        return true;
    }
}