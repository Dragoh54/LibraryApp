using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using LibraryApp.DataAccess.Dto;

namespace LibraryApp.Application.Mapper;

public class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<UserDto, UserEntity>.NewConfig()
            .Map(dest => dest.Nickname, src => src.Nickname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role);

        TypeAdapterConfig<BookDto, BookDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ISBN, src => src.ISBN)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Genre, src => src.Genre)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.TakenAt, src => src.TakenAt)
            .Map(dest => dest.ReturnBy, src => src.ReturnBy)
            .Map(dest => dest.AuthorId, src => src.AuthorId);

        TypeAdapterConfig<CreateBookDto, BookDto>.NewConfig()
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Genre, src => src.Genre);

        TypeAdapterConfig<AuthorDto, AuthorEntity>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Books, src => src.Books.Select(book => book.Adapt<BookDto>()).ToList());

        TypeAdapterConfig<CreateAuthorDto, AuthorDto>.NewConfig()
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.BirthDate, src => src.BirthDate);
    }
}
