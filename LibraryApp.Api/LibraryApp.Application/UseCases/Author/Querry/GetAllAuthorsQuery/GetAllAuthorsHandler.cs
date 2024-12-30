using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetAllAuthorsQuery;

public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAuthorsHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.AuthorRepository.GetAll(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (!authors.Any())
        {
            throw new NotFoundException("No authors found");
        }
        
        return authors.Adapt<IEnumerable<AuthorDto>>();
    }
}