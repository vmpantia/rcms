using AutoMapper;
using RCMS.Domain.Entities;
using RCMS.Shared.Models.Courses;

namespace RCMS.Application.Courses;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CourseCategory, CourseCategoryDto>()
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
    }
}