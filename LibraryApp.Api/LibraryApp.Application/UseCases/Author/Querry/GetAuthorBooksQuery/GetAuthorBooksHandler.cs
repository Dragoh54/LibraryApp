using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetBooksByAuthorQuery;

public class GetAuthorBooksHandler : IRequestHandler<GetAuthorBooksQuery, IEnumerable<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorBooksHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<IEnumerable<BookDto>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.AuthorRepository.GetAuthorBooks(request.Id);
        cancellationToken.ThrowIfCancellationRequested();

        if (books is null)
        {
            throw new NotFoundException("Author's books with this id doesn't exist");
        }

        return books.Adapt<IEnumerable<BookDto>>();
    }
}