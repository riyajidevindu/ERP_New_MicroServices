using AutoMapper;
using ERP.GraduateManagement.Core.DTOs.Responses;
using ERP.GraduateManagement.Core.Entities;

namespace ERP.GraduateManagement.Api.Mapping_Profiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {

            CreateMap<Graduate, GetGraduateResponse>()
                .ForMember(
                dest => dest.GraduateId, opt => opt.MapFrom(src => src.Id))
                .ForMember(
                dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Graduate, GetGraduateByIdResponse>()
               .ForMember(
               dest => dest.GraduateId, opt => opt.MapFrom(src => src.Id));
        }       
    }
}

