using FluentValidation;
using FluentValidation.Results;
using LibraryApp.Application.Services;
using LibraryApp.DataAccess.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AuthorController : Controller
{
    private readonly AuthorService _authorService;
    private readonly IValidator<CreateAuthorDto> _validator;

    public AuthorController(AuthorService authorService, IValidator<CreateAuthorDto> validator)
    {
        _authorService = authorService;
        _validator = validator;
    }

    [HttpGet]
    [Route("/authors")]
    public async Task<IResult> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthors();

        return Results.Ok(authors);
    }

    [HttpGet]
    [Route("/authors/{id:Guid}")]
    public async Task<IResult> GetAuthorById([FromRoute]Guid id)
    {
        var author = await _authorService.GetAuthorById(id);

        return Results.Ok(author);
    }

    [HttpPost]
    [Route("/authors/add")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddAuthor([FromBody] CreateAuthorDto authorDto)
    {
        ValidationResult result = await _validator.ValidateAsync(authorDto);
        if (!result.IsValid)
        {
            return Results.NoContent();
        }

        var author = await _authorService.AddAuthor(authorDto);
        return Results.Ok(author);
    }

    [HttpPut]
    [Route("/authors/update/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateAuthor([FromRoute]Guid id, [FromBody] CreateAuthorDto authorDto)
    {
        ValidationResult result = await _validator.ValidateAsync(authorDto);
        if (!result.IsValid)
        {
            return Results.NoContent();
        }

        var newAuthor = await _authorService.UpdateAuthor(id, authorDto);
        return Results.Ok(newAuthor);
    }

    [HttpDelete]
    [Route("/authors/delete/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteAuthor([FromRoute]Guid id)
    {
        var deletedAuthor = await _authorService.DeleteAuthor(id);
        return Results.Ok(deletedAuthor);
    }

    [HttpGet]
    [Route("/authors/{id:Guid}/books")]
    public async Task<IResult> GetBooksByAuthor([FromRoute]Guid id)
    {
        var books = await _authorService.GetAuthorBooks(id);
        return Results.Ok(books);
    }
    
    [HttpGet]
    [Route("/authors/list/")]
    public async Task<IResult> GetPaginatedAuthors([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var paginatedAuthors = await _authorService.GetPaginatedAuthors(page, pageSize);
        return Results.Ok(paginatedAuthors);
    }


}

