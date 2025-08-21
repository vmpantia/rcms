using RCMS.Infrastructure.DataAccess.Entities.Contracts;
using RCMS.Shared.Enumerations;

namespace RCMS.Infrastructure.DataAccess.Entities;

public class Instructor : IPersonalInfoEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public InstructorStatus Status { get; set; }
    public ICollection<Course> Courses { get; set; }
}