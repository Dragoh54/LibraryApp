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

public class BookService(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<BookDto>> GetAllBooks()
    {
        var books = await _unitOfWork.BookRepository.GetAll();
        return books.Adapt<IEnumerable<BookDto>>();
    }

    public async Task<BookDto> GetBookById(Guid id)
    {
        var book = await _unitOfWork.BookRepository.Get(id);

        if(book is null)
        {
            throw new Exception("Book with this id doesn't exist");
        }

        return book.Adapt<BookDto>();
    }

    public async Task<BookDto> GetBookByIsbn(string isbn)
    {
        var book = await _unitOfWork.BookRepository.GetByISBN(isbn);

        if (book is null)
        {
            throw new Exception("Book with this isbn doesn't exist");
        }

        return book.Adapt<BookDto>();
    }

    public async Task<BookDto> AddBook(BookDto bookDto)
    {
        if (bookDto is null)
        {
            throw new Exception("This book is null");
        }

        var author = await _unitOfWork.AuthorRepository.Get(bookDto.AuthorId);
        if (author is null)
        {
            throw new Exception("This book author doesn't exist.");
        }

        var book = new BookDto
        {
            Id = bookDto.Id,
            ISBN = bookDto.ISBN,
            Title = bookDto.Title,
            Description = bookDto.Description,
            Genre = bookDto.Genre,
            AuthorId = author.Id,
            TakenAt = bookDto.TakenAt ?? DateTime.MinValue, 
            ReturnBy = bookDto.ReturnBy ?? DateTime.MinValue,
        };

        await _unitOfWork.BookRepository.Add(book.Adapt<BookEntity>());

        return bookDto;
    }
}
