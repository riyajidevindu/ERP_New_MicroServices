using AutoMapper;
using ERP.LabEquipmentManagement.Core.DTOs.Requests;
using ERP.LabEquipmentManagement.Core.Entities;

namespace ERP.LabEquipmentManagement.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<UpdateLabEquipmentRequest, LabEquipment>()
                 .ForMember(
                dest => dest.Status, opt => opt.MapFrom(src => 1))
                 .ForMember(
                dest => dest.AddDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabEquipmentRequest, LabEquipment>()
               .ForMember(
               dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

