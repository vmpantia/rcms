using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Models.Instructors;

public class InstructorLiteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Initials { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public InstructorStatus Status { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}