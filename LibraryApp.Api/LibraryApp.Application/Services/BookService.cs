﻿using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using LibraryApp.DomainModel.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.Services;

public class BookService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooks()
    {
        var books = await _unitOfWork.BookRepository.GetAll();
        
        if (!books.Any())
        {
            throw new NotFoundException("No books found");
        }
        
        return books.Adapt<IEnumerable<BookDto>>();
    }

    public async Task<BookDto> GetBookById(Guid id)
    {
        var book = await _unitOfWork.BookRepository.Get(id);

        if(book is null)
        {
            throw new NotFoundException("Book with this id doesn't exist");
        }

        return book.Adapt<BookDto>();
    }

    public async Task<BookDto> GetBookByIsbn(string isbn)
    {
        var book = await _unitOfWork.BookRepository.GetByISBN(isbn);
        if (book is null)
        {
            throw new NotFoundException("Book with this isbn doesn't exist");
        }

        return book.Adapt<BookDto>();
    }

    public async Task<CreateBookDto> AddBook(CreateBookDto newBookDto)
    {
        var tempBook = await _unitOfWork.BookRepository.GetByISBN(newBookDto.ISBN);
        if (tempBook != null)
        {
            throw new AlreadyExistsException("A book with this ISBN already exists.");
        }
        
        var author = await _unitOfWork.AuthorRepository.Get(newBookDto.AuthorId);
        if (author is null)
        {
            throw new NotFoundException("This book author doesn't exist.");
        }

        var book = tempBook.Adapt<BookDto>();

        await _unitOfWork.BookRepository.Add(book.Adapt<BookEntity>());
        await _unitOfWork.BookRepository.SaveAsync();

        return newBookDto;
    }

    public async Task<BookDto> DeleteBook(Guid id)
    {
        var book = await _unitOfWork.BookRepository.Get(id);

        if (book is null)
        {
            throw new NotFoundException("Book with this id doesn't exist");
        }
        
        await _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.BookRepository.SaveAsync();

        return book.Adapt<BookDto>();
    }

    public async Task<BookDto> UpdateBook(Guid id, CreateBookDto bookDto)
    {
        _ = await _unitOfWork.AuthorRepository.Get(id) ?? 
            throw new NotFoundException("Book with this id doesn't exist");
        
        var existingBook = await _unitOfWork.BookRepository.GetByISBN(bookDto.ISBN);
        if (existingBook != null)
        {
            throw new AlreadyExistsException("A book with this ISBN already exists.");
        }
        
        var updatedBook = bookDto.Adapt<BookDto>();
        updatedBook.Id = id;

        await _unitOfWork.BookRepository.Update(updatedBook.Adapt<BookEntity>());
        await _unitOfWork.BookRepository.SaveAsync();
        
        return updatedBook;
    }
    
    public async Task<bool> TakeBook(Guid bookId, string userIdClaim)
    {
        var userId = Guid.Parse(userIdClaim);
    
        _ = await _unitOfWork.UserRepository.Get(userId)
                   ?? throw new NotFoundException("User not found.");

        var book = await _unitOfWork.BookRepository.Get(bookId)
                   ?? throw new NotFoundException("Book not found.");

        if (book.UserId.HasValue)
        {
            throw new BadRequestException("This book is already taken.");
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
        if (page <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and pageSize must be greater than zero.");
        }

        var (items, totalCount) = await _unitOfWork.BookRepository.GetBooks(page, pageSize);

        if (items is null)
        {
            throw new NotFoundException("No books found");
        }

        var books = items.Adapt<List<BookDto>>();

        return new PaginatedPagedResult<BookDto>
        {
            Items = books,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}
