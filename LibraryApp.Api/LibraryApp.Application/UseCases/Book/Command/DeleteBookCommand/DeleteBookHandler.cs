using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.DeleteBookCommand;

public class DeleteBookHandler: IRequestHandler<DeleteBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    public async Task<BookDto> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.Get(request.Id);
        cancellationToken.ThrowIfCancellationRequested();

        if (book is null)
        {
            throw new NotFoundException("Book with this id doesn't exist");
        }
        
        await _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.SaveChangesAsync();

        cancellationToken.ThrowIfCancellationRequested();
        return book.Adapt<BookDto>();
    }
}