using AutoMapper;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;
using ERP.LabScheduleManagement.Core.Entities;

namespace ERP.LabScheduleManagement.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateTimeSlotRequest, TimeSlot>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
;


            CreateMap<UpdateTimeSlotRequest, TimeSlot>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.TimeSlotId))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow)); 
        }
    }
}
