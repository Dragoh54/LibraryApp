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
        if (newBookDto is null)
        {
            throw new Exception("This book is null");
        }

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
        if (bookDto is null)
        {
            throw new Exception("This book is null");
        }

        if (string.IsNullOrWhiteSpace(bookDto.ISBN))
        {
            throw new Exception("This book doesn't have an ISBN");
        }

        if (string.IsNullOrWhiteSpace(bookDto.Title))
        {
            throw new Exception("This book doesn't have a title");
        }

        if (string.IsNullOrWhiteSpace(bookDto.Description))
        {
            throw new Exception("This book doesn't have a description");
        }

        if (string.IsNullOrWhiteSpace(bookDto.Genre))
        {
            throw new Exception("This book doesn't have a genre");
        }

        if (bookDto.AuthorId == Guid.Empty)
        {
            throw new Exception("This book doesn't have an author");
        }
        
        var updatedBook = bookDto.Adapt<BookDto>();
        updatedBook.Id = id;

        await _unitOfWork.BookRepository.Update(updatedBook.Adapt<BookEntity>());
        await _unitOfWork.BookRepository.SaveAsync();
        
        return updatedBook;
    }

    //TODO: take userId to set it to book
    public async Task<BookDto> TakeBook(Guid id, TakeBookRequest bookRequest, string? userId)
    {
        if (id == Guid.Empty)
        {
            throw new Exception("This book doesn't have an ID");
        }

        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("Incorrect user ID");
        }
    
        if (bookRequest is null)
        {
            throw new Exception("This book request is null");
        }
    
        if (bookRequest.ReturnDate < DateTime.Today)
        {
            throw new Exception("Incorrect return date");
        }
        
        var book = await _unitOfWork.BookRepository.Get(id);
        var user = await _unitOfWork.UserRepository.Get(Guid.Parse(userId));

        if (book is null)
        {
            throw new Exception("Book with this id doesn't exist");
        }

        if (user is null)
        {
            throw new Exception("Incorrect user ID");
        }
        
        book.TakenAt = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc);
        book.ReturnBy = DateTime.SpecifyKind(bookRequest.ReturnDate, DateTimeKind.Utc);
        book.UserId = user.Id;
        book.User = user;
        
        await _unitOfWork.BookRepository.Update(book);
        await _unitOfWork.BookRepository.SaveAsync();
        
        return book.Adapt<BookDto>();
    }
}
