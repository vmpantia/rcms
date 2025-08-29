using AutoMapper;
using RCMS.Domain.Entities;
using RCMS.Infrastructure.Extensions;
using RCMS.Shared.Models.Instructors;

namespace RCMS.Application.Instructors;

public class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        CreateMap<Instructor, InstructorLiteDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.GetFormattedName()))
            .ForMember(dst => dst.Initials, opt => opt.MapFrom(src => src.GetInitials()))
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
        CreateMap<Instructor, InstructorDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.GetFormattedName()))
            .ForMember(dst => dst.Initials, opt => opt.MapFrom(src => src.GetInitials()))
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
        CreateMap<CreateInstructorDto, Instructor>();
        CreateMap<UpdateInstructorDto, Instructor>();
    }
}