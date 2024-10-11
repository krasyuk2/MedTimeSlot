namespace MedTimeSlot.Api.DTOs.Schedule;

public record RemoveScheduleRequest(int IdDoctor, DayOfWeek DayOfWeek);