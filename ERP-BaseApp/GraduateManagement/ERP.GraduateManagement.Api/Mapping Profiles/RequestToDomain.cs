using AutoMapper;
using ERP.GraduateManagement.Core.DTOs.Requests;
using ERP.GraduateManagement.Core.Entities;

namespace ERP.GraduateManagement.Api.Mapping_Profiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateGraduateRequest, Graduate>()
                 .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1))
                 .ForMember(
                dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateGraduateRequest, Graduate>()
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.GraduateId))
               .ForMember(
               dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1));
        }
    }
}
