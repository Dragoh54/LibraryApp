using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Services;

public class AuthorService(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
    {
        var authors = await _unitOfWork.AuthorRepository.GetAll();
        return authors.Adapt<IEnumerable<AuthorDto>>();
    }

    public async Task<AuthorDto> GetAuthorById(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.Get(id);

        if (author is null)
        {
            throw new Exception("Author with this id doesn't exist");
        }

        return author.Adapt<AuthorDto>();
    }

    public async Task<IEnumerable<BookDto>> GetAuthorBooks(Guid id)
    {
        var books = await _unitOfWork.AuthorRepository.GetAuthorBooks(id);

        if (books is null)
        {
            throw new Exception("Author with this id doesn't exist");
        }

        return books.Adapt<IEnumerable<BookDto>>();
    }

    public async Task<AuthorDto> AddAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = createAuthorDto.Adapt<AuthorDto>(); 
        await _unitOfWork.AuthorRepository.Add(author.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();

        return author;
    }

    public async Task<AuthorDto> UpdateAuthor(Guid id, CreateAuthorDto authorDto)
    {
        var updatedAuthor = authorDto.Adapt<AuthorDto>();
        updatedAuthor.Id = id;
        
        await _unitOfWork.AuthorRepository.Update(updatedAuthor.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();

        return updatedAuthor;
    }

    public async Task<AuthorDto> DeleteAuthor(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.Get(id);

        if (author is null)
        {
            throw new Exception("Author with this id doesn't exist");
        }
        
        await _unitOfWork.AuthorRepository.Delete(author);
        await _unitOfWork.AuthorRepository.SaveAsync();
        
        return author.Adapt<AuthorDto>();
    }
    
    public async Task<PaginatedPagedResult<AuthorDto>> GetPaginatedAuthors(int page, int pageSize)
    {
        var paginatedAuthors = await _unitOfWork.AuthorRepository.GetAuthors(page, pageSize);
        
        var authorsDto = paginatedAuthors.Items.Adapt<List<AuthorDto>>();

        return new PaginatedPagedResult<AuthorDto>
        {
            Items = authorsDto,
            TotalCount = paginatedAuthors.TotalCount,
            Page = page,
            PageSize = pageSize
        };
    }

}
