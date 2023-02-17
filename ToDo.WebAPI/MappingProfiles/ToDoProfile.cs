using AutoMapper;
using ToDo.WebAPI.DTOs;

namespace ToDo.WebAPI.MappingProfiles
{
    public class ToDoProfile : Profile 
    {
        public ToDoProfile()
        {
            CreateMap<Core.Entities.ToDo, ToDoDto>();

            CreateMap<ToDoForCreateUpdateDto, Core.Entities.ToDo>();
        }
    }
}
