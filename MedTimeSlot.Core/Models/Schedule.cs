namespace MedTimeSlot.Core.Models;

public class Schedule
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public List<CellTime> Times { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public TimeSpan AppointmentDuration { get; set; }
    public DateTimeOffset StartLunchTime { get; set; }
    public DateTimeOffset EndLunchTime { get; set; }
}