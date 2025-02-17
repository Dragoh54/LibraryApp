﻿using LibraryApp.Application.Interfaces.UnitOfWork;
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
        _ = await _unitOfWork.AuthorRepository.Get(request.Id, cancellationToken) ?? 
            throw new NotFoundException("Author with this id doesn't exist");
        
        await _unitOfWork.AuthorRepository.Update(request.Adapt<AuthorEntity>(), cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        return request.Adapt<AuthorDto>();
    }
}