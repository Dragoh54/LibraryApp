using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;

namespace LibraryApp.Application.Mapper;

public class BookDtoConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<BookDto, BookEntity>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ISBN, src => src.ISBN)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Genre, src => src.Genre)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.TakenAt, src => src.TakenAt)
            .Map(dest => dest.ReturnBy, src => src.ReturnBy)
            .Map(dest => dest.AuthorId, src => src.AuthorId);
        
        TypeAdapterConfig<UpdateBookDto, BookDto>.NewConfig()
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Genre, src => src.Genre)
            .Map(dest => dest.ISBN, src => src.ISBN);
        
        TypeAdapterConfig<CreateBookDto, BookDto>.NewConfig()
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Genre, src => src.Genre);
    }
}