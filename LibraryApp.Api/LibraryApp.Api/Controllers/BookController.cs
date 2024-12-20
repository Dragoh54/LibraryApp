using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using LibraryApp.Application.Filters;
using LibraryApp.Application.Services;
using LibraryApp.Application.UseCases.Book.Command.AddBookCommand;
using LibraryApp.Application.UseCases.Book.Command.DeleteBookCommand;
using LibraryApp.Application.UseCases.Book.Command.TakeBookCommand;
using LibraryApp.Application.UseCases.Book.Command.UpdateBookCommand;
using LibraryApp.Application.UseCases.Book.Querry.GetAllBooksQuery;
using LibraryApp.Application.UseCases.Book.Querry.GetBookByIdQuery;
using LibraryApp.Application.UseCases.Book.Querry.GetBookByIsbnQuery;
using LibraryApp.Application.UseCases.Book.Querry.GetPaginatedBooksQuery;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookController : Controller
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/books/all")]
    public async Task<IResult> GetAllBooks(CancellationToken cancellationToken)
    {
        var books = await _mediator.Send(new GetAllBooksQuery(), cancellationToken);
        return Results.Ok(books);
    }

    [HttpGet]
    [Route("/books/")]
    public async Task<IResult> GetBookById([FromQuery]GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await _mediator.Send(query, cancellationToken);
        return Results.Ok(book);
    }

    [HttpGet]
    [Route("/books/isbn/")]
    public async Task<IResult> GetBookByIsbn([FromQuery]GetBookByIsbnQuery query, CancellationToken cancellationToken)
    {
        var book = await _mediator.Send(query, cancellationToken);
        return Results.Ok(book);
    }

    [HttpPost]
    [Route("/books/add")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> AddBook([FromBody] AddBookCommand bookDto, CancellationToken token)
    {
        var book = await _mediator.Send(bookDto, token);
        return Results.Ok(book);
    }

    [HttpDelete]
    [Route("/books/delete/")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> DeleteBook([FromQuery] DeleteBookCommand bookDto, CancellationToken token)
    {
        var book = await _mediator.Send(bookDto, token);
        return Results.Ok(book);
    }

    [HttpPut]
    [Route("/books/update/")]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> UpdateBook([FromForm]UpdateBookCommand bookDto, CancellationToken token)
    {
        var book = await _mediator.Send(bookDto, token);
        return Results.Ok(book);
    }

    [HttpPost]
    [Route("/books/take")]
    public async Task<IResult> TakeBook([FromQuery] TakeBookCommand bookDto, CancellationToken token)
    {
        bookDto.UserClaimId = User.FindFirst("Id")?.Value;
        var success = await _mediator.Send(bookDto, token);
        return Results.Ok(success);
    }
    
    [HttpGet]
    [Route("/books/list")]
    public async Task<IResult> GetBooks([FromQuery] GetPaginatedBooksQuery query, CancellationToken token)
    {
        var paginatedBooks = await _mediator.Send(query, token);
        return Results.Ok(paginatedBooks);
    }
}

