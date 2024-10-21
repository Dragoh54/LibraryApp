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
        TypeAdapterConfig<UserEntity, UserDto>.NewConfig()
            .Map(dest => dest.Nickname, src => src.Nickname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role);
    }
}
