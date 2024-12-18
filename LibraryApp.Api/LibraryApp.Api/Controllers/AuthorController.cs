using FluentValidation;
using FluentValidation.Results;
using LibraryApp.Application.Services;
using LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;
using LibraryApp.DataAccess.Dto;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AuthorController : Controller
{
    private readonly AuthorService _authorService;
    private readonly IMediator _mediator;

    public AuthorController(AuthorService authorService, IMediator mediator)
    {
        _authorService = authorService;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
    public async Task<IResult> AddAuthor([FromBody] AddAuthorCommand authorDto, CancellationToken token)
    {
        var author = await _mediator.Send(authorDto, token);
        return Results.Ok(author);
    }

    // [HttpPost]
    // [Route("/authors/add")]
    // [Authorize(Policy = "Admin")]
    // public async Task<IResult> AddAuthor([FromBody] CreateAuthorDto authorDto)
    // {
    //     var author = await _authorService.AddAuthor(authorDto);
    //     return Results.Ok(author);
    // }

    [HttpPut]
    [Route("/authors/update/{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateAuthor([FromRoute]Guid id, [FromBody] CreateAuthorDto authorDto)
    {
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

