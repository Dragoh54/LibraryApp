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
    public async Task<IResult> AddBook([FromBody] BookDto bookDto)
    {
        return Results.Ok();
    }

    [HttpDelete]
    [Route("/books/delete/{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteBook(int id)
    {
        return Results.Ok();
    }

    [HttpPut]
    [Route("/books/update/{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateBook(int id, [FromBody] BookDto bookDto)
    {
        return Results.Ok();
    }

    [HttpPost]
    [Route("/books/issue/{bookId:int}/user/{userId:int}")]
    public async Task<IResult> IssueBookToUser(int bookId, int userId)
    {
        return Results.Ok();
    }
}

