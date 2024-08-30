using AutoMapper;
using ERP.ModuleRegistration.Core.DTOs.Request;
using ERP.ModuleRegistration.Core.Entity;

namespace ERP.StudentRegistration.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {

        public RequestToDomain()
        {
            CreateMap<CreateModuleRequest, Module>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
               .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

                      
            CreateMap<UpdateModuleRequest, Module>()
               .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }

    }
}
