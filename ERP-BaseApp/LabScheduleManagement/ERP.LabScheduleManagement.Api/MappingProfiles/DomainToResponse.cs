using AutoMapper;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.Entities;

namespace ERP.LabScheduleManagement.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            //Time Slot mappings
            CreateMap<TimeSlot, GetTimeSlotResponse>()
                .ForMember(dest => dest.TimeSlotId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.StartTime)))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.EndTime)))
                .ForMember(dest => dest.BookedDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BookedDate)))
                .ForMember(dest => dest.RescheduledDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime((DateTime)src.RescheduledDate)));

            CreateMap<TimeSlot, GetTimeSlotByIdResponse>()
                .ForMember(dest => dest.TimeSlotId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.StartTime)))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.EndTime)))
                .ForMember(dest => dest.BookedDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BookedDate)))
                .ForMember(dest => dest.RescheduledDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime((DateTime)src.RescheduledDate)));

            //Student Mappings
            CreateMap<Student, GetStudentResponse>()
               .ForMember(dest => dest.StudentId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<Student, GetStudentByIdResponse>()
               .ForMember(dest => dest.StudentId,
                   opt => opt.MapFrom(src => src.Id));

            //Lab Mapping
            CreateMap<Lab, GetLabResponse>()
               .ForMember(dest => dest.LabId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<Lab, GetLabByIdResponse>()
                .ForMember(dest => dest.LabId,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
