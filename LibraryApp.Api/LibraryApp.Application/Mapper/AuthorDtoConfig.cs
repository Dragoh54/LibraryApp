using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;

namespace LibraryApp.Application.Mapper;

public class AuthorDtoConfig
{
    public static void RegisterMappings()
    {
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
        
        TypeAdapterConfig<UpdateAuthorDto, AuthorDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.BirthDate, src => src.BirthDate);
    }
}