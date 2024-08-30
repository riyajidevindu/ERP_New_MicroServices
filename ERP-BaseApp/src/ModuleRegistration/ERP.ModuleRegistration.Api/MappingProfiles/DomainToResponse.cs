using AutoMapper;
using ERP.ModuleRegistration.Core.DTOs.Response;
using ERP.ModuleRegistration.Core.Entity;

namespace ERP.StudentRegistration.Api.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<Module, ModuleResponse>()          
           .ForMember(dest => dest.ModuleId,opt => opt.MapFrom(src => src.Id));
            
    }


}
