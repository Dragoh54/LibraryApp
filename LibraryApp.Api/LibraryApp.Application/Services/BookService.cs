using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Services;

public class BookService(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<BookEntity>> GetAllBooks()
    {
        var books = await _unitOfWork.BookRepository.GetAll();
        return books;
    }

    public async Task<BookEntity> GetBookById(Guid id)
    {
        var book = await _unitOfWork.BookRepository.Get(id);

        if(book is null)
        {
            throw new Exception("Book with this id doesn't exist");
        }

        return book;
    }

    public async Task<BookEntity> GetBookByIsbn(string isbn)
    {
        var book = await _unitOfWork.BookRepository.GetByISBN(isbn);

        if (book is null)
        {
            throw new Exception("Book with this isbn doesn't exist");
        }

        return book;
    }
}
