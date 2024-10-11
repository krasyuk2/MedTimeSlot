namespace MedTimeSlot.Core.Models;

public class Doctor : Person
{
    public string Speciality { get; set; }
    public List<Schedule> Schedules { get; set; }
}