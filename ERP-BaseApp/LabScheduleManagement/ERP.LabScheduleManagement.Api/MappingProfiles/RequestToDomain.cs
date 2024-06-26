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

            //Lab equipments
            CreateMap<CreateLabEquipmentRequest, LabEquipment>()
               .ForMember(dest => dest.Status,
                   opt => opt.MapFrom(src => 1))
               .ForMember(dest => dest.AddedDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabEquipmentRequest, LabEquipment>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.LabEquipmentId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));


            //Lab Group
            CreateMap<CreateLabGroupRequest, LabGroup>()
               .ForMember(dest => dest.Status,
                   opt => opt.MapFrom(src => 1))
               .ForMember(dest => dest.AddedDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabGroupRequest, LabGroup>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.LabGroupId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            //Lab Instructor
            CreateMap<CreateLabInstructorRequest, LabInstructor>()
              .ForMember(dest => dest.Status,
                  opt => opt.MapFrom(src => 1))
              .ForMember(dest => dest.AddedDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow))
              .ForMember(dest => dest.UpdateDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabInstructorRequest, LabInstructor>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.LabInstructorId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            //Lab Space
            CreateMap<CreateLabSpaceRequest, LabSpace>()
              .ForMember(dest => dest.Status,
                  opt => opt.MapFrom(src => 1))
              .ForMember(dest => dest.AddedDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow))
              .ForMember(dest => dest.UpdateDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateLabSpaceRequest, LabSpace>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.LabSpaceId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            //Module
            CreateMap<CreateModuleRequest, Module>()
              .ForMember(dest => dest.Status,
                  opt => opt.MapFrom(src => 1))
              .ForMember(dest => dest.AddedDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow))
              .ForMember(dest => dest.UpdateDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateModuleRequest, Module>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.ModuleId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

            //Scheduled Labs
            CreateMap<CreateScheduledLabRequest, ScheduledLab>()
              .ForMember(dest => dest.Status,
                  opt => opt.MapFrom(src => 1))
              .ForMember(dest => dest.AddedDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow))
              .ForMember(dest => dest.UpdateDate,
                  opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateScheduledLabRequest, ScheduledLab>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.ScheduledLabId))
               .ForMember(dest => dest.UpdateDate,
                   opt => opt.MapFrom(src => DateTime.UtcNow));

        }
    }
}
