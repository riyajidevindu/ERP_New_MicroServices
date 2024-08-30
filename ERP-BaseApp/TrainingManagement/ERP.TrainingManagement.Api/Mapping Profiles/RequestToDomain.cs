using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Requests;
using ERP.TrainingManagement.Core.Entities;
using System;

namespace ERP.TrainingManagement.Api.Mapping_Profiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateInternshipVacancyRequest, InternshipVacancy>()
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
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                    dest => dest.status,
                    opt => opt.MapFrom(src => 1));

            CreateMap<CreateApprovalRequest, ApprovalRequest>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateApprovalRequest, ApprovalRequest>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateInternshipVacancyRequest, InternshipVacancy>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
