using AutoMapper;
using ERP.StudentRegistration.Core.DTO.Response;
using ERP.StudentRegistration.Core.DTOs.Response;
using ERP.StudentRegistration.Core.Entity;

namespace ERP.StudentRegistration.Api.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<Student, StudentResponse>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
           .ForMember(dest => dest.StudentId,opt => opt.MapFrom(src => src.Id));
            
    }


}
