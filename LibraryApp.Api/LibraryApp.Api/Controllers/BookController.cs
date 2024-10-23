using LibraryApp.DataAccess.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookController : Controller
{
    [HttpGet]
    [Route("/books")]
    public async Task<IResult> GetAllBooks()
    {
        return Results.Ok();
    }

    [HttpGet]
    [Route("/books/{id:int}")]
    public async Task<IResult> GetBookById(int id)
    {
        return Results.Ok();
    }

    [HttpGet]
    [Route("/books/isbn/{isbn}")]
    public async Task<IResult> GetBookByIsbn(string isbn)
    {
        return Results.Ok();
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

