using LibraryApp.Application.Filters;
using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetPaginatedAuthorsQuery;

public class GetPaginatedAuthorsHandler : IRequestHandler<GetPaginatedAuthorsQuery, PaginatedPagedResult<AuthorDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedAuthorsHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<PaginatedPagedResult<AuthorDto>> Handle(GetPaginatedAuthorsQuery request, CancellationToken cancellationToken)
    {
        (int pageNumber, int pageSize) = (request.PageNumber, request.PageSize);
        
        var (items, totalCount) = await _unitOfWork.AuthorRepository.GetAuthors(request.Filters, pageNumber, pageSize, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        if (items is null)
        {
            throw new NotFoundException("There are no authors in the database.");
        }
        
        var authors = items.Adapt<List<AuthorDto>>();

        return new PaginatedPagedResult<AuthorDto>
        {
            Items = authors,
            TotalCount = totalCount,
            Page = pageNumber,
            PageSize = pageSize
        };
    }
}