using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using LibraryApp.Application.Filters;
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

    public BookController(BookService bookService, IValidator<CreateBookDto> validator)
    {
        _bookService = bookService;
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
    public async Task<IResult> GetBookById([FromRoute]Guid id)
    {
        var book = await _bookService.GetBookById(id);
        return Results.Ok(book);
    }

    [HttpGet]
    [Route("/books/isbn/{isbn}")]
    public async Task<IResult> GetBookByIsbn([FromRoute]string isbn)
    {
        var book = await _bookService.GetBookByIsbn(isbn);
        return Results.Ok(book);
    }

    [HttpPost]
    [Route("/books/add")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddBook([FromBody] CreateBookDto bookDto)
    {
        var book = await _bookService.AddBook(bookDto);
        return Results.Ok(book);
    }

    [HttpDelete]
    [Route("/books/delete/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteBook([FromRoute]Guid id)
    {
        var deletedBook = await _bookService.DeleteBook(id);
        return Results.Ok(deletedBook);
    }

    [HttpPut]
    [Route("/books/update/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateBook([FromRoute]Guid id, [FromBody] CreateBookDto bookDto)
    {
        var updatedBook = await _bookService.UpdateBook(id, bookDto);
        return Results.Ok(updatedBook);
    }

    [HttpPost]
    [Route("/books/{bookId:Guid}/take")]
    public async Task<IResult> TakeBook([FromRoute]Guid bookId)
    {
        string? userIdClaim = User.FindFirst("Id")?.Value;
        var success = await _bookService.TakeBook(bookId, userIdClaim);
        return Results.Ok(success);
    }
    
    [HttpGet]
    [Route("/books/list")]
    public async Task<IResult> GetBooks([FromQuery]BookFilters filters, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var paginatedBooks = await _bookService.GetBooks(filters, page, pageSize);
        return Results.Ok(paginatedBooks);
    }
}

