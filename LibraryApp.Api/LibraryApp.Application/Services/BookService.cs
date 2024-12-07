﻿using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Application.Book;
using Microsoft.AspNetCore.Http;

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

    public async Task<CreateBookDto> AddBook(CreateBookDto newBookDto)
    {
        var author = await _unitOfWork.AuthorRepository.Get(newBookDto.AuthorId);
        if (author is null)
        {
            throw new Exception("This book author doesn't exist.");
        }

        var book = newBookDto.Adapt<BookDto>();

        await _unitOfWork.BookRepository.Add(book.Adapt<BookEntity>());

        return newBookDto;
    }

    public async Task<BookDto> DeleteBook(Guid id)
    {
        var book = await _unitOfWork.BookRepository.Get(id);

        if (book is null)
        {
            throw new Exception("Book with this id doesn't exist");
        }
        
        await _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.BookRepository.SaveAsync();

        return book.Adapt<BookDto>();
    }

    public async Task<BookDto> UpdateBook(Guid id, CreateBookDto bookDto)
    {
        var updatedBook = bookDto.Adapt<BookDto>();
        updatedBook.Id = id;

        await _unitOfWork.BookRepository.Update(updatedBook.Adapt<BookEntity>());
        await _unitOfWork.BookRepository.SaveAsync();
        
        return updatedBook;
    }
    
    public async Task<bool> TakeBook(Guid bookId, Guid userId)
    {
        var book = await _unitOfWork.BookRepository.Get(bookId);

        if (book == null)
        {
            throw new Exception("Book not found.");
        }

        if (book.UserId.HasValue)
        {
            return false;
        }

        book.TakenAt = DateTime.UtcNow;
        book.ReturnBy = DateTime.UtcNow.AddMonths(1);
        book.UserId = userId;

        await _unitOfWork.BookRepository.Update(book);
        await _unitOfWork.BookRepository.SaveAsync();

        return true; 
    }
    
    public async Task<PaginatedPagedResult<BookDto>?> GetBooks(int page, int pageSize)
    {
        var paginatedBooks = await _unitOfWork.BookRepository.GetBooks(page, pageSize);
        if (paginatedBooks is null)
        {
            return null;
        }

        return new PaginatedPagedResult<BookDto>
        {
            Items = paginatedBooks.Items.Adapt<List<BookDto>>(),
            TotalCount = paginatedBooks.TotalCount,
            Page = paginatedBooks.Page,
            PageSize = paginatedBooks.PageSize
        };
    }
}
