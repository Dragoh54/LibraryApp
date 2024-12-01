﻿using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using LibraryApp.Application.Book;
using LibraryApp.Application.Services;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookController : Controller
{
    private readonly BookService _bookService;
    private readonly IValidator<CreateBookDto> _validator;

    public BookController(BookService bookService, IValidator<CreateBookDto> validator)
    {
        _bookService = bookService;
        _validator = validator;
    }

    [HttpGet]
    [Route("/books")]
    public async Task<IResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooks();

        return Results.Ok(books);
    }

    [HttpGet]
    [Route("/books/{id:Guid}")]
    public async Task<IResult> GetBookById(Guid id)
    {
        var book = await _bookService.GetBookById(id);

        return Results.Ok(book);
    }

    [HttpGet]
    [Route("/books/isbn/{isbn}")]
    public async Task<IResult> GetBookByIsbn(string isbn)
    {
        var book = await _bookService.GetBookByIsbn(isbn);

        return Results.Ok(book);
    }

    [HttpPost]
    [Route("/books/add")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddBook([FromBody] CreateBookDto bookDto)
    {
        ValidationResult result = await _validator.ValidateAsync(bookDto);
        
        if (!result.IsValid)
        {
            return Results.NoContent();
        }
        
        var book = await _bookService.AddBook(bookDto);
        return Results.Ok(book);
    }

    [HttpDelete]
    [Route("/books/delete/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteBook(Guid id)
    {
        var deletedBook = await _bookService.DeleteBook(id);
        return Results.Ok(deletedBook);
    }

    [HttpPut]
    [Route("/books/update/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateBook(Guid id, [FromBody] CreateBookDto bookDto)
    {
        ValidationResult result = await _validator.ValidateAsync(bookDto);
        if (!result.IsValid)
        {
            return Results.NoContent();
        }
        
        var updatedBook = await _bookService.UpdateBook(id, bookDto);
        return Results.Ok(updatedBook);
    }

    [HttpPost]
    [Route("/books/take/{id:Guid}")]
    public async Task<IResult> TakeBook(Guid id, [FromBody] TakeBookRequest bookRequest)
    {
        var userId = User.FindFirst("Id")?.Value;
        var book = await _bookService.TakeBook(id, bookRequest, userId);
        
        
        return Results.Ok(book);
    }
}

