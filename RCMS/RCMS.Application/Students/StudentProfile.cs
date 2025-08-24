using AutoMapper;
using RCMS.Domain.Entities;
using RCMS.Infrastructure.Extensions;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Students;

namespace RCMS.Application.Students;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentLiteDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.GetFormattedName()))
            .ForMember(dst => dst.Initials, opt => opt.MapFrom(src => src.GetInitials()))
            .ForMember(dst => dst.NoOfOngoingEnrollments, opt => opt.MapFrom(src => src.Enrollments.Count(e => e.Status == EnrollmentStatus.InProgress)))
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
        CreateMap<Student, StudentDto>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.GetFormattedName()))
            .ForMember(dst => dst.Initials, opt => opt.MapFrom(src => src.GetInitials()))
            .ForMember(dst => dst.NoOfOngoingEnrollments, opt => opt.MapFrom(src => src.Enrollments.Count(e => e.Status == EnrollmentStatus.InProgress)))
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
        CreateMap<CreateStudentDto, Student>();
        
    }
}