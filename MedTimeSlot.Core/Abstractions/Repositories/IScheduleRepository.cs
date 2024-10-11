using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions.Repositories;

public interface IScheduleRepository
{
    Task<List<CellTime>> CreateBaseSchedules(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime);

    Task<List<Schedule>> GetAllSchedules(int doctorId);
    Task<List<CellTime>> GetCellTimesByDate(int doctorId, DateTimeOffset date);

    Task<List<Schedule>> PutSchedule(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime);

    Task<List<Schedule>> RemoveSchedule(int idDoctor, DayOfWeek dayOfWeek);
}