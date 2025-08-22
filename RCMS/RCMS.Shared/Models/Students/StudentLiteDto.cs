using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Models.Students;

public class StudentLiteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Initials { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public StudentStatus Status { get; set; }
    public int NoOfOngoingEnrollments { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}