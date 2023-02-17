using AutoMapper;
using ToDo.WebAPI.DTOs;
using ToDo.Core.Entities;

namespace ToDo.WebAPI.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserRegistrationDto, User>();
        }
    }
}
