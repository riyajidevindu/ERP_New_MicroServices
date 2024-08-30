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

            //Lab Coordinator 
            CreateMap<LabCoordinator, GetLabCoordinatorResponse>()
             .ForMember(dest => dest.LabCoordinatorId,
                 opt => opt.MapFrom(src => src.Id));

            CreateMap<LabCoordinator, GetLabCoordinatorByIdResponse>()
             .ForMember(dest => dest.LabCoordinatorId,
                 opt => opt.MapFrom(src => src.Id));

            //Lab Equipments
            CreateMap<LabEquipment, GetLabEquipmentResponse>()
             .ForMember(dest => dest.LabEquipmentId,
                 opt => opt.MapFrom(src => src.Id));

            CreateMap<LabEquipment, GetLabEquipmentByIdResponse>()
             .ForMember(dest => dest.LabEquipmentId,
                 opt => opt.MapFrom(src => src.Id));

            //Lab Group
            CreateMap<LabGroup, GetLabGroupResponse>()
                 .ForMember(dest => dest.LabGroupId,
                     opt => opt.MapFrom(src => src.Id));

            CreateMap<LabGroup, GetLabGroupByIdResponse>()
                 .ForMember(dest => dest.LabGroupId,
                     opt => opt.MapFrom(src => src.Id));


            //Lab Instructor
            CreateMap<LabInstructor, GetLabInstructorResponse>()
               .ForMember(dest => dest.LabInstructorId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<LabInstructor, GetLabInstructorByIdResponse>()
              .ForMember(dest => dest.LabInstructorId,
                  opt => opt.MapFrom(src => src.Id));

            //Lab Space
            CreateMap<LabSpace, GetLabSpaceResponse>()
               .ForMember(dest => dest.LabSpaceId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<LabSpace, GetLabSpaceByIdResponse>()
              .ForMember(dest => dest.LabSpaceId,
                  opt => opt.MapFrom(src => src.Id));

            //Module
            CreateMap<Module, GetModuleResponse>()
               .ForMember(dest => dest.ModuleId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<Module, GetModuleByIdResponse>()
               .ForMember(dest => dest.ModuleId,
                   opt => opt.MapFrom(src => src.Id));

            //Scheduled Labs
            CreateMap<ScheduledLab, GetScheduledLabResponse>()
               .ForMember(dest => dest.ScheduledLabId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<ScheduledLab, GetScheduledLabByIdResponse>()
               .ForMember(dest => dest.ScheduledLabId,
                   opt => opt.MapFrom(src => src.Id));

        }
    }
}
