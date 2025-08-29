using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Models.Instructors;

public class InstructorDto : InstructorLiteDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime BirthDate { get; set; }
}