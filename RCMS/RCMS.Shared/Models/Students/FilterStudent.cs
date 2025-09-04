namespace RCMS.Shared.Models.Students;

public class FilterStudent
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public IEnumerable<string>? Genders { get; set; }
    public IEnumerable<string>? Statuses { get; set; }
}