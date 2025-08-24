using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Interfaces.Entities;

public interface IPersonalInfoEntity
{
    string FirstName { get; set; }
    string? MiddleName { get; set; }
    string LastName { get; set; }
    GenderType Gender { get; set; }
    DateTime BirthDate { get; set; }
    string PhoneNumber { get; set; }
    string EmailAddress { get; set; }
}