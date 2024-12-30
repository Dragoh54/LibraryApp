using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetBookByIdQuery;

public class GetBookByIdHandler: IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByIdHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.Get(request.Id, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        if(book is null)
        {
            throw new NotFoundException("Book with this id doesn't exist");
        }

        return book.Adapt<BookDto>();
    }
}