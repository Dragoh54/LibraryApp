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

    public async Task<IEnumerable<AuthorEntity>> GetAllAuthors()
    {
        var authors = await _unitOfWork.AuthorRepository.GetAll();
        return authors;
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

        return books;
    }
}
