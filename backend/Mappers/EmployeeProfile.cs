using AutoMapper;
using backend.DTO.request;
using backend.DTO.response;
using backend.Entity;

namespace backend.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<AddEmployeeDTO, Employee>();
            CreateMap<Employee, GetEmployeeDTO>();
        }
    }
}
