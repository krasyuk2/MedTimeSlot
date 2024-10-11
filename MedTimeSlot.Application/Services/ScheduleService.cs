using MedTimeSlot.Core.Abstractions;
using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _repository;

    public ScheduleService(IScheduleRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<CellTime>> CreateBaseSchedules(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime)
    {
        return await _repository.CreateBaseSchedules(idDoctor, dayOfWeek, startTime, endTime, appointmentDuration,
            startLaunchTime, endLaunchTime);
    }
    
    public async Task<List<Schedule>> GetAllSchedules(int doctorId)
    {
        return await _repository.GetAllSchedules(doctorId);
    }

    public async Task<List<CellTime>> GetCellTimesByDate(int doctorId, DateTimeOffset date)
    {
        return await _repository.GetCellTimesByDate(doctorId, date);
    }

    public async Task<List<Schedule>> PutSchedule(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime)
    {
        return await _repository.PutSchedule(idDoctor, dayOfWeek, startTime, endTime, appointmentDuration,
            startLaunchTime, endLaunchTime);
    }
    
    public Task<List<Schedule>> RemoveSchedule(int idDoctor, DayOfWeek dayOfWeek)
    {
        return _repository.RemoveSchedule(idDoctor, dayOfWeek);
    }
}