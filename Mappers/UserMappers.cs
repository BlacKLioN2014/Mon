using AutoMapper;
using Mon.Models;
using Mon.Models.Dtos;

namespace Mon.Mappers
{
    public class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<AppUsuario, UserDatosDto>().ReverseMap();
        }
    }
}
