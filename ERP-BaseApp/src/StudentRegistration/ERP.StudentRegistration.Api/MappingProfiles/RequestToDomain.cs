using AutoMapper;
using ERP.StudentRegistration.Core.DTO.Request;
using ERP.StudentRegistration.Core.Entity;

namespace ERP.StudentRegistration.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {

        public RequestToDomain()
        {
            CreateMap<CreateStudentRequest, Student>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
               .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

                      
            CreateMap<UpdateStudentRequest, Student>()
               .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }

    }
}
