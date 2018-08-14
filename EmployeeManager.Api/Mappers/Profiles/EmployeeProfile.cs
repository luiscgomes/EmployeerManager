using AutoMapper;
using EmployeeManager.Api.Contracts;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.ValueOjects;

namespace EmployeeManager.Api.Mappers.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeCreateModel, Employee>()
                .ForMember(d => d.Id, o => o.UseValue(0))
                .ForMember(d => d.Email, o => o.MapFrom(s => new Email(s.Email)))
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<Employee, EmployeeModel>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Address));
        }
    }
}