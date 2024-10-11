namespace MedTimeSlot.Api.DTOs.Schedule;

public record CreateScheduleRequest(
    int IdDoctor,
    DayOfWeek DayOfWeek,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    TimeSpan AppointmentDuration,
    DateTimeOffset StartLaunchTime,
    DateTimeOffset EndLaunchTime);