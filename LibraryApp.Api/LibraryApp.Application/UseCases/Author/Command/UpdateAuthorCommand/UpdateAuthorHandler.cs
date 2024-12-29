using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using LibraryApp.Entities.Models;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.UpdateAuthorCommand;

public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = await _unitOfWork.AuthorRepository.Get(request.Id) ?? 
            throw new NotFoundException("Author with this id doesn't exist");

        var updatedAuthor = request.Adapt<AuthorDto>();
        
        await _unitOfWork.AuthorRepository.Update(updatedAuthor.Adapt<AuthorEntity>());
        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        return updatedAuthor;
    }
}