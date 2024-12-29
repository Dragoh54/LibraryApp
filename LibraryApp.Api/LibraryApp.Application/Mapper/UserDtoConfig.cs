using LibraryApp.DataAccess.Dto;
using LibraryApp.Entities.Models;
using Mapster;

namespace LibraryApp.Application.Mapper;

public class UserDtoConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<UserDto, UserEntity>.NewConfig()
            .Map(dest => dest.Nickname, src => src.Nickname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role);
        
        TypeAdapterConfig<UserEntity, UserDto>.NewConfig()
            .Map(dest => dest.Nickname, src => src.Nickname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role);
    }
}