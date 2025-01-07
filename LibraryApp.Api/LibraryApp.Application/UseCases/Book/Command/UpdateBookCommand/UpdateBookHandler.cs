using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using LibraryApp.Entities.Models;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.UpdateBookCommand;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        _ = await _unitOfWork.BookRepository.Get(request.Id, cancellationToken) ?? 
            throw new NotFoundException("Book with this id doesn't exist");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var existingBook = await _unitOfWork.BookRepository.GetByISBN(request.ISBN, cancellationToken);
        if (existingBook != null)
        {
            throw new AlreadyExistsException("A book with this ISBN already exists.");
        }

        await _unitOfWork.BookRepository.Update(request.Adapt<BookEntity>(), cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        return request.Adapt<BookDto>();
    }
}