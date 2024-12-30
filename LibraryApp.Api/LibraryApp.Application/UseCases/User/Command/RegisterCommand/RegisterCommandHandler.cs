using LibraryApp.Application.Interfaces.Auth;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Enums;
using LibraryApp.DomainModel.Exceptions;
using LibraryApp.Entities.Models;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.User.Command.RegisterCommand;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public RegisterCommandHandler(IUnitOfWork appUnitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _unitOfWork = appUnitOfWork;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _unitOfWork.UserRepository.GetByEmail(request.Email, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        if(candidate is not null)
        {
            throw new AlreadyExistsException("User with this email already exists!");
        }

        var hashedPassword = _passwordHasher.Generate(request.Password, cancellationToken);

        var user = new UserEntity(Guid.NewGuid(), request.Nickname, request.Email, hashedPassword, Role.User);

        await _unitOfWork.UserRepository.Add(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        return user.Adapt<UserDto>();
    }
}