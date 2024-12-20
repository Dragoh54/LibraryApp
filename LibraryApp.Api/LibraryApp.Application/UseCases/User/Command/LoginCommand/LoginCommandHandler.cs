using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel.Exceptions;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.LoginCommand;

public class LoginCommandHandler : IRequestHandler<LoginCommand, (string, string)>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUnitOfWork appUnitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _unitOfWork = appUnitOfWork;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<(string, string)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userByEmail = await _unitOfWork.UserRepository.GetByEmail(request.Email);
        cancellationToken.ThrowIfCancellationRequested();

        if (userByEmail is null)
        {
            throw new NotFoundException("Cannot found user with this email");
        }

        var result = _passwordHasher.Verify(request.Password, userByEmail.PasswordHash);

        if (!result)
        {
            throw new BadRequestException("Failed to login");
        }

        var token = _jwtProvider.GenerateAccessToken(userByEmail);
        var refreshToken = _jwtProvider.GenerateRefreshToken(userByEmail);
        
        if (refreshToken is null || token is null)
        {
            throw new UnauthorizedAccessException("Failed to generate tokens.");
        }
        
        await _unitOfWork.RefreshTokenRepository.Add(refreshToken);
        await _unitOfWork.RefreshTokenRepository.SaveAsync();

        cancellationToken.ThrowIfCancellationRequested();
        return (token, refreshToken.Id.ToString());
    }
}