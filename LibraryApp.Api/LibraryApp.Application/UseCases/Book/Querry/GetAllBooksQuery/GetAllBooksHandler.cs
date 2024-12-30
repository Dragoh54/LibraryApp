using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetAllBooksQuery;

public class GetAllBooksHandler: IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBooksHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.BookRepository.GetAll(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (!books.Any())
        {
            throw new NotFoundException("No books found");
        }
        
        return books.Adapt<IEnumerable<BookDto>>();
    }
}