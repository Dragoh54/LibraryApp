﻿using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using LibraryApp.Entities.Models;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.AddBookCommand;

public class AddBookHandler : IRequestHandler<AddBookCommand, BookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }
    
    public async Task<BookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var tempBook = await _unitOfWork.BookRepository.GetByISBN(request.ISBN);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (tempBook != null)
        {
            throw new AlreadyExistsException("A book with this ISBN already exists.");
        }
        
        var author = await _unitOfWork.AuthorRepository.Get(request.AuthorId);
        cancellationToken.ThrowIfCancellationRequested();
        
        if (author is null)
        {
            throw new NotFoundException("This book author doesn't exist.");
        }

        var book = request.Adapt<BookDto>();

        await _unitOfWork.BookRepository.Add(book.Adapt<BookEntity>());
        await _unitOfWork.BookRepository.SaveAsync();

        cancellationToken.ThrowIfCancellationRequested();
        return book;
    }
}