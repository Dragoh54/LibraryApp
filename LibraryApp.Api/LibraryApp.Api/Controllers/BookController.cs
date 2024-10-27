using Azure.Core;
using LibraryApp.Application.Services;
using LibraryApp.DataAccess.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
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
        var book = await _bookService.AddBook(bookDto);
        return Results.Ok(book);
    }

    [HttpDelete]
    [Route("/books/delete/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteBook(Guid id)
    {
        return Results.Ok();
    }

    [HttpPut]
    [Route("/books/update/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateBook(Guid id, [FromBody] BookDto bookDto)
    {
        return Results.Ok();
    }

    [HttpPost]
    [Route("/books/take/{bookId:int}")]
    public async Task<IResult> TakeBook(Guid bookId)
    {
        return Results.Ok();
    }

    [HttpPost]
    [Route("/books/taken")]
    public async Task<IResult> GetUserBooks()
    {
        return Results.Ok();
    }
}

