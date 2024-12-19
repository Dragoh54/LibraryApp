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
        
        if (pageNumber <= 0 || pageSize <= 0)
        {
            throw new BadRequestException("Page and pageSize must be greater than zero.");
        }
        var (items, totalCount) = await _unitOfWork.AuthorRepository.GetAuthors(pageNumber, pageSize);
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