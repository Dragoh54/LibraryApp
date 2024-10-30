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
        if (string.IsNullOrWhiteSpace(createAuthorDto.Surname))
        {
            throw new ArgumentException("Surname can't be empty", nameof(createAuthorDto.Surname));
        }
        
        if (string.IsNullOrWhiteSpace(createAuthorDto.Country))
        {
            throw new ArgumentException("Country can't be empty", nameof(createAuthorDto.Country));
        }

        if ( createAuthorDto.BirthDate > DateTime.Today)
        {
            throw new ArgumentException("Invalid date of birth", nameof(createAuthorDto.BirthDate));
        }

        var author = createAuthorDto.Adapt<AuthorDto>(); 
        await _unitOfWork.AuthorRepository.Add(author.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();

        return author;
    }

    public async Task<AuthorDto> UpdateAuthor(Guid id, CreateAuthorDto authorDto)
    {
        if (authorDto is null)
        {
            throw new Exception("Author with this id doesn't exist");
        }
        
        if (string.IsNullOrWhiteSpace(authorDto.Surname))
        {
            throw new ArgumentException("Surname can't be empty", nameof(authorDto.Surname));
        }
        
        if (string.IsNullOrWhiteSpace(authorDto.Country))
        {
            throw new ArgumentException("Country can't be empty", nameof(authorDto.Country));
        }

        if ( authorDto.BirthDate > DateTime.Today)
        {
            throw new ArgumentException("Invalid date of birth", nameof(authorDto.BirthDate));
        }

        var updatedAuthor = authorDto.Adapt<AuthorDto>();
        updatedAuthor.Id = id;
        
        await _unitOfWork.AuthorRepository.Update(updatedAuthor.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();
        
        Console.WriteLine(updatedAuthor.Surname);

        return updatedAuthor;
    }

    public async Task<AuthorDto> DeleteAuthor(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.Get(id);

        if (author is null)
        {
            throw new KeyNotFoundException("Автор с указанным идентификатором не найден.");
        }
        
        await _unitOfWork.AuthorRepository.Delete(author);
        await _unitOfWork.AuthorRepository.SaveAsync();
        
        return author.Adapt<AuthorDto>();
    }
}
