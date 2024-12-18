using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.DeleteAuthorCommand;

public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<AuthorDto> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.AuthorRepository.Get(request.Id);

        if (author is null)
        {
            throw new NotFoundException("Author with this id doesn't exist");
        }
        
        await _unitOfWork.AuthorRepository.Delete(author);
        await _unitOfWork.AuthorRepository.SaveAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return author.Adapt<AuthorDto>();
    }
}