using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DomainModel.Exceptions;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.TakeBookCommand;

public class TakeBookHandler : IRequestHandler<TakeBookCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public TakeBookHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<bool> Handle(TakeBookCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserClaimId);
    
        _ = await _unitOfWork.UserRepository.Get(userId, cancellationToken)
            ?? throw new NotFoundException("User not found.");
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _unitOfWork.BookRepository.Get(request.Id, cancellationToken)
                   ?? throw new NotFoundException("Book not found.");
        cancellationToken.ThrowIfCancellationRequested();

        if (book.UserId.HasValue)
        {
            throw new BadRequestException("This book is already taken.");
        }

        book.TakenAt = DateTime.UtcNow;
        book.ReturnBy = DateTime.UtcNow.AddMonths(1);
        book.UserId = userId;

        await _unitOfWork.BookRepository.Update(book, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        cancellationToken.ThrowIfCancellationRequested();
        return true;
    }
}