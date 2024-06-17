using AutoMapper;
using ERP.WorkLoadManagement.Core.DTOs.Response;
using ERP.WorkLoadManagement.Core.Entities;

namespace ERP.WorkLoadManagement.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Work, GetWorkResponse>()
                .ForMember(dest=>dest.WorkId, opt=>opt.MapFrom(src=>src.Id));

            CreateMap<Work, GetWorkByIdResponse>()
                .ForMember(dest => dest.WorkId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
