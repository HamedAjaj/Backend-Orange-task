using AutoMapper;
using Orange_task.Domain.Entities;
using Orange_task.DTOs;

namespace Orange_task.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
