using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Responses;
using ERP.TrainingManagement.Core.Entities;

namespace ERP.TrainingManagement.Api.Mapping_Profiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<InternshipVacancy, GetInternshipVacancyResponse>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.Company,
                    opt => opt.MapFrom(src => src.Company))
           
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.MapFrom(src => src.ModifiedDate));
        }
    }
}
