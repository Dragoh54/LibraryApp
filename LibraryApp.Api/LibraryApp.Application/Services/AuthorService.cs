﻿using LibraryApp.Application.Interfaces.UnitOfWork;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using LibraryApp.DomainModel.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.Services;

public class AuthorService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
    {
        var authors = await _unitOfWork.AuthorRepository.GetAll();

        if (!authors.Any())
        {
            throw new NotFoundException("No authors found");
        }
        
        return authors.Adapt<IEnumerable<AuthorDto>>();
    }

    public async Task<AuthorDto> GetAuthorById(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.Get(id);

        if (author is null)
        {
            throw new NotFoundException("Author with this id doesn't exist");
        }

        return author.Adapt<AuthorDto>();
    }

    public async Task<IEnumerable<BookDto>> GetAuthorBooks(Guid id)
    {
        var books = await _unitOfWork.AuthorRepository.GetAuthorBooks(id);

        if (books is null)
        {
            throw new NotFoundException("Author's books with this id doesn't exist");
        }

        return books.Adapt<IEnumerable<BookDto>>();
    }

    public async Task<AuthorDto> AddAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = createAuthorDto.Adapt<AuthorDto>(); 
        await _unitOfWork.AuthorRepository.Add(author.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();

        return author;
    }

    public async Task<AuthorDto> UpdateAuthor(Guid id, CreateAuthorDto authorDto)
    {
        _ = await _unitOfWork.AuthorRepository.Get(id) ?? 
            throw new NotFoundException("Author with this id doesn't exist");

        var updatedAuthor = authorDto.Adapt<AuthorDto>();
        updatedAuthor.Id = id;
        
        await _unitOfWork.AuthorRepository.Update(updatedAuthor.Adapt<AuthorEntity>());
        await _unitOfWork.AuthorRepository.SaveAsync();

        return updatedAuthor;
    }

    public async Task<AuthorDto> DeleteAuthor(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.Get(id);

        if (author is null)
        {
            throw new NotFoundException("Author with this id doesn't exist");
        }
        
        await _unitOfWork.AuthorRepository.Delete(author);
        await _unitOfWork.AuthorRepository.SaveAsync();
        
        return author.Adapt<AuthorDto>();
    }
    
    public async Task<PaginatedPagedResult<AuthorDto>> GetPaginatedAuthors(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            throw new BadRequestException("Page and pageSize must be greater than zero.");
        }
        var (items, totalCount) = await _unitOfWork.AuthorRepository.GetAuthors(page, pageSize);

        if (items is null)
        {
            throw new NotFoundException("There are no authors in the database.");
        }
        
        var authors = items.Adapt<List<AuthorDto>>();

        return new PaginatedPagedResult<AuthorDto>
        {
            Items = authors,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

}
