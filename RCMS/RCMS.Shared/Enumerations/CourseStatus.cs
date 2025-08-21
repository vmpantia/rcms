namespace RCMS.Shared.Enumerations;

public enum CourseStatus
{
    Draft,            // Being created, not yet published
    Published,        // Visible and open for enrollment
    Ongoing,          // Actively running with enrolled students
    Completed,        // Finished and archived
    Cancelled,        // Called off before or during execution
    Archived          // Hidden from public view but retained for records
}