using API_JWT.Model;
using API_JWT.Model.DTO;
using AutoMapper;

namespace API_JWT.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<UserDTO, CreateUserDTO>().ReverseMap();
        }
    }
}
