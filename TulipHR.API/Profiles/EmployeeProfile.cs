using AutoMapper;

namespace TulipHR.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() {
          
            CreateMap<Entities.Employee, Models.EmployeeDTO>();
            CreateMap<Models.EmployeeDTO, Entities.Employee>();
            CreateMap<Models.EmployeeCreationDto, Entities.Employee>();
        }
    }
}
