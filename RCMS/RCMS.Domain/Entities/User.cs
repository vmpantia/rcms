using RCMS.Domain.Interfaces.Entities;
using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Entities;

public class User : BaseEntity, IPersonalInfoEntity
{
    public Guid Id { get; set; }
    public Guid? StudentOrInstructorId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
}