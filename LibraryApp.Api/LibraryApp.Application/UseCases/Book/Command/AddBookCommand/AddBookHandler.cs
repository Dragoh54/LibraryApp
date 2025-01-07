using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using LibraryApp.Entities.Models;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.AddBookCommand;

public class AddBookHandler : IRequestHandler<AddBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<BookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var tempBook = await _unitOfWork.BookRepository.GetByISBN(request.ISBN, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (tempBook != null)
        {
            throw new AlreadyExistsException("A book with this ISBN already exists.");
        }
        
        var author = await _unitOfWork.AuthorRepository.Get(request.AuthorId, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (author is null)
        {
            throw new NotFoundException("This book author doesn't exist.");
        }

        await _unitOfWork.BookRepository.Add(request.Adapt<BookEntity>(), cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        cancellationToken.ThrowIfCancellationRequested();
        return request.Adapt<BookDto>();
    }
}