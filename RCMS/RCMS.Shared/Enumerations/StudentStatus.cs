namespace RCMS.Shared.Enumerations;

public enum StudentStatus
{
    Active,           // Currently enrolled or eligible to enroll
    Inactive,         // Temporarily disengaged (e.g., on leave)
    Graduated,        // Completed all required courses
    Suspended,        // Temporarily blocked due to policy violations
    Withdrawn,        // Voluntarily left the review center
    Banned            // Permanently removed from the system
}