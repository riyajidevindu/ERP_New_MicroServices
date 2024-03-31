using AutoMapper;
using ERP.LabEquipmentManagement.Core.DTOs.Requests;
using ERP.LabEquipmentManagement.Core.DTOs.Responses;
using ERP.LabEquipmentManagement.Core.Entities;
using System.Data;

namespace ERP.LabEquipmentManagement.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<LabEquipment, GetLabEquipmentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
                

        
        }
    }
}

