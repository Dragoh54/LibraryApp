using FluentValidation;
using FluentValidation.Results;
using LibraryApp.Application.Dto;
using LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;
using LibraryApp.Application.UseCases.Author.Command.DeleteAuthorCommand;
using LibraryApp.Application.UseCases.Author.Command.UpdateAuthorCommand;
using LibraryApp.Application.UseCases.Author.Querry.GetAllAuthorsQuery;
using LibraryApp.Application.UseCases.Author.Querry.GetAuthorByIdQuerry;
using LibraryApp.Application.UseCases.Author.Querry.GetBooksByAuthorQuery;
using LibraryApp.Application.UseCases.Author.Querry.GetPaginatedAuthorsQuery;
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
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("/authors/all")]
    public async Task<IResult> GetAllAuthors(CancellationToken cancellationToken)
    {
        var authors = await _mediator.Send(new GetAllAuthorsQuery(), cancellationToken);
        return Results.Ok(authors);
    }
    
    [HttpGet]
    [Route("/authors/")]
    public async Task<IResult> GetAuthorById([FromQuery]GetAuthorByIdQuery query, CancellationToken cancellationToken)
    {
        var author = await _mediator.Send(query, cancellationToken);
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
    
    [HttpPut]
    [Route("/authors/update/")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateAuthor([FromForm]UpdateAuthorCommand authorDto, CancellationToken token)
    {
        var author = await _mediator.Send(authorDto, token);
        return Results.Ok(author);
    }
    
    [HttpDelete]
    [Route("/authors/delete/")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteAuthor([FromQuery] DeleteAuthorCommand authorDto, CancellationToken token)
    {
        var author = await _mediator.Send(authorDto, token);
        return Results.Ok(author);
    }
    
    [HttpGet]
    [Route("/authors/books")]
    public async Task<IResult> GetBooksByAuthor([FromQuery]GetAuthorBooksQuery query, CancellationToken token)
    {
        var books = await _mediator.Send(query, token);
        return Results.Ok(books);
    }
    
    [HttpGet]
    [Route("/authors/list/")]
    public async Task<IResult> GetPaginatedAuthors([FromQuery] GetPaginatedAuthorsQuery query, CancellationToken token)
    {
        var paginatedAuthors = await _mediator.Send(query, token);
        return Results.Ok(paginatedAuthors);
    }
}

