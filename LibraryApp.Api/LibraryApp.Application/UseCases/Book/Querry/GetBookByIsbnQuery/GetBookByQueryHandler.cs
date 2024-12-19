using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIsbnQuery;

public class GetBookByQueryHandler: IRequestHandler<GetBookByIsbnQuery, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByQueryHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    public async Task<BookDto> Handle(GetBookByIsbnQuery request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.GetByISBN(request.Isbn);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (book is null)
        {
            throw new NotFoundException("Book with this isbn doesn't exist");
        }

        return book.Adapt<BookDto>();
    }
}