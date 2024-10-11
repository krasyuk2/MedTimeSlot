namespace MedTimeSlot.Api.DTOs.Schedule;

public record GetScheduleRequest(int IdDoctor, DateTimeOffset Date);