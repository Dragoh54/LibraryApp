using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;

public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<AuthorDto> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var author = request.Adapt<AuthorDto>(); 
        await _unitOfWork.AuthorRepository.Add(author.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();

        return author;
    }
}