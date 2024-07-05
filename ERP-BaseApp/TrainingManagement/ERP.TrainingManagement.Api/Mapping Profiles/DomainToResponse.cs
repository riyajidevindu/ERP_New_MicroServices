using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Requests;
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
                    opt => opt.MapFrom(src => src.Id));
            CreateMap<ApprovalRequest, GetApprovalRequestResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));



        }
    }
}
