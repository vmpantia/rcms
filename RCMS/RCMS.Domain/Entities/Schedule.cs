namespace RCMS.Domain.Entities;

public class Schedule : BaseEntity
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    
    public virtual CourseSession Session { get; set; }
}