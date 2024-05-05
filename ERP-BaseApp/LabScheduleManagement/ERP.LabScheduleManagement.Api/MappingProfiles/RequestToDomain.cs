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
            //Time Slot Mappings
            CreateMap<CreateTimeSlotRequest, TimeSlot>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateTimeSlotRequest, TimeSlot>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.TimeSlotId))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            //Student Mappings
            CreateMap<CreateStudentRequest, Student>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateStudentRequest, Student>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.StudentId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            //Lab Mapping
            CreateMap<CreateLabRequest, Lab>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabRequest, Lab>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.LabId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            //Lab Coordinator
            CreateMap<CreateLabCoordinatorRequest, LabCoordinator>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabCoordinatorRequest, LabCoordinator>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.LabCoordinatorId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

        }
    }
}
