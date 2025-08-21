namespace RCMS.Shared.Enumerations;

public enum EnrollmentStatus
{
    Pending,         // Awaiting approval or payment
    Confirmed,       // Fully enrolled and ready to attend
    InProgress,      // Actively attending sessions
    Completed,       // Finished the course successfully
    Dropped,         // Voluntarily left the course
    Cancelled,       // Enrollment revoked or invalidated
    OnHold,          // Temporarily paused (e.g., due to personal reasons)
    Transferred      // Moved to another course or batch
}