namespace RCMS.Shared.Models.Students;

public class CreateStudentDto
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}