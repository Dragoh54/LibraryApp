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
}
