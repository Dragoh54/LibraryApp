using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Querry.GetAuthorByIdQuerry;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorByIdHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.AuthorRepository.Get(request.Id);
        cancellationToken.ThrowIfCancellationRequested();

        if (author is null)
        {
            throw new NotFoundException("Author with this id doesn't exist");
        }

        return author.Adapt<AuthorDto>();
    }
}