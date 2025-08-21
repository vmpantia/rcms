namespace RCMS.Shared.Enumerations;

public enum InstructorStatus
{    
    Active,           // Currently teaching or available for assignment
    Inactive,         // Temporarily unavailable (e.g., on leave)
    Suspended,        // Restricted due to policy or performance issues
    Retired,          // No longer teaching but retained for records
    Terminated,       // Permanently removed from the system
    PendingApproval,  // Awaiting onboarding or admin verification
    Archived          // Hidden from active views but retained for audit/history
}