using AutoMapper;
using ERP.WorkLoadManagement.Core.DTOs.Request;
using ERP.WorkLoadManagement.Core.Entities;

namespace ERP.WorkLoadManagement.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateWorkRequest,Work>()
                .ForMember(dest=>dest.Status,opt=>opt.MapFrom(src=>1))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateWorkRequest,Work>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Id, opt=>opt.MapFrom(src=>src.WorkId))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CreateStaffRequest, Staff>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateStaffRequest, Staff>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CreateAssignWorkRequest, AssignWork>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateAssignWorkRequest, AssignWork>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AssignedWorkId))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
