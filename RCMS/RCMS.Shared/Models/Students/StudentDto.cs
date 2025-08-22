using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Models.Students;

public class StudentDto : StudentLiteDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime BirthDate { get; set; }
}