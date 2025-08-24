namespace RCMS.Domain.Entities;

public class Schedule : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    
    public virtual Course Course { get; set; }
}