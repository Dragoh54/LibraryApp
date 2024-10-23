using LibraryApp.DataAccess.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AuthorController : Controller
{
    [HttpGet]
    [Route("/authors")]
    public async Task<IResult> GetAllAuthors()
    {
        return Results.Ok();
    }

    [HttpGet]
    [Route("/authors/{id:int}")]
    public async Task<IResult> GetAuthorById(int id)
    {
        return Results.Ok();
    }

    [HttpPost]
    [Route("/authors/add")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddAuthor([FromBody] AuthorDto authorDto)
    {
        return Results.Ok();
    }

    [HttpPut]
    [Route("/authors/update/{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
    {
        return Results.Ok();
    }

    [HttpDelete]
    [Route("/authors/delete/{id:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteAuthor(int id)
    {
        return Results.Ok();
    }

    [HttpGet]
    [Route("/authors/{id:int}/books")]
    public async Task<IResult> GetBooksByAuthor(int id)
    {
        return Results.Ok();
    }
}

