namespace MedTimeSlot.Core.Models;

public class CellTime
{
    public int Id { get; set; }
    public DateTimeOffset Time { get; set; }
    public int? PatientId { get; set; }
    public int ScheduleId { get; set; }
}