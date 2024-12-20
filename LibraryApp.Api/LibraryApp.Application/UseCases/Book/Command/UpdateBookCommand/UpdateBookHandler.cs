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
        _ = await _unitOfWork.BookRepository.Get(request.Id) ?? 
            throw new NotFoundException("Book with this id doesn't exist");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var existingBook = await _unitOfWork.BookRepository.GetByISBN(request.ISBN);
        if (existingBook != null)
        {
            throw new AlreadyExistsException("A book with this ISBN already exists.");
        }
        
        var updatedBook = request.Adapt<BookDto>();

        await _unitOfWork.BookRepository.Update(updatedBook.Adapt<BookEntity>());
        await _unitOfWork.BookRepository.SaveAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        return updatedBook;
    }
}