﻿using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.DomainModel.Exceptions;
using Mapster;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Querry.GetPaginatedBooksQuery;

public class GetPaginatedBooksHandler : IRequestHandler<GetPaginatedBooksQuery, PaginatedPagedResult<BookDto>> 
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaginatedBooksHandler(IUnitOfWork appUnitOfWork)
    {
        _unitOfWork = appUnitOfWork;
    }

    public async Task<PaginatedPagedResult<BookDto>> Handle(GetPaginatedBooksQuery request, CancellationToken cancellationToken)
    {
        (int pageNumber, int pageSize) = (request.PageNumber, request.PageSize);
        
        if (pageNumber <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and pageSize must be greater than zero.");
        }

        var (items, totalCount) = await _unitOfWork.BookRepository.GetBooks(request.Filters, pageNumber, pageSize);

        if (items is null)
        {
            throw new NotFoundException("No books found");
        }

        var books = items.Adapt<List<BookDto>>();

        return new PaginatedPagedResult<BookDto>
        {
            Items = books,
            TotalCount = totalCount,
            Page = pageNumber,
            PageSize = pageSize
        };
    }
}