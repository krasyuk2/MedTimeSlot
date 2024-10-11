using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;
using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MedTimeSlot.DataAccess.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly MedicalTimeSlotDbContext _context;

    public ScheduleRepository(MedicalTimeSlotDbContext context)
    {
        _context = context;
    }

    public async Task<List<CellTime>> CreateBaseSchedules(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime)
    {
        var doctor = await _context.Doctors.FindAsync(idDoctor);
        var schedules = await _context.Schedules
            .Where(s => s.DoctorId == idDoctor && s.StartTime.Date == startTime.Date.ToUniversalTime())
            .ToListAsync();
        if (schedules.Any()) throw new TimeException("Schedules already exists");
        if (doctor == null) throw new NotFoundException("Doctor not found");

        if (startTime > endTime || startLaunchTime > endLaunchTime || startLaunchTime > endTime ||
            startLaunchTime < startTime || endLaunchTime > endTime || endTime < startTime ||
            endTime < endLaunchTime)
            throw new TimeException(
                $"Check if the time is correct - {startTime} - {endTime}, and {startLaunchTime} - {endLaunchTime}");

        if (dayOfWeek != startTime.DayOfWeek)
            throw new TimeException($"Start date is incorrect. {dayOfWeek} - {startTime.DayOfWeek}");

        var allCellTimes = new List<CellTime>();

        for (var i = 0; i < 3; i++)
        {
            var schedule = new Schedule
            {
                DayOfWeek = dayOfWeek,
                DoctorId = doctor.Id,
                StartTime = startTime.AddDays(i * 7),
                EndTime = endTime.AddDays(i * 7),
                AppointmentDuration = appointmentDuration,
                StartLunchTime = startLaunchTime.AddDays(i * 7),
                EndLunchTime = endLaunchTime.AddDays(i * 7)
            };

            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();

            var cellTimes = GenerateCellTimes(startTime.AddDays(i * 7), endTime.AddDays(i * 7),
                appointmentDuration, startLaunchTime.AddDays(i * 7),
                endLaunchTime.AddDays(i * 7), schedule.Id);

            _context.CellTimes.AddRange(cellTimes);
            await _context.SaveChangesAsync();

            allCellTimes.AddRange(cellTimes);
        }

        return allCellTimes;
    }

    public async Task<List<Schedule>> GetAllSchedules(int doctorId)
    {
        var doctor = await _context.Doctors.FindAsync(doctorId);
        if (doctor == null) throw new NotFoundException("Doctor not found");
        var listSchedules = await _context.Schedules.Where(s => s.DoctorId == doctorId).ToListAsync();
        return listSchedules;
    }

    public async Task<List<CellTime>> GetCellTimesByDate(int doctorId, DateTimeOffset date)
    {
        var schedules = await GetAllSchedules(doctorId);
        if (schedules.Count == 0) throw new NotFoundException("Schedule not found");
        var cellTimes = new List<CellTime>();
        foreach (var schedule in schedules)
        {
            var scheduleCellTimes = await _context.CellTimes
                .Where(ct =>
                    ct.ScheduleId == schedule.Id && ct.Time.Date == date.Date.ToUniversalTime() && ct.PatientId == null)
                .ToListAsync();
            cellTimes.AddRange(scheduleCellTimes);
        }

        if (!cellTimes.Any() || cellTimes.Count == 0) throw new NotFoundException("Cell Time not found");
        return cellTimes;
    }

    public async Task<List<Schedule>> RemoveSchedule(int idDoctor, DayOfWeek dayOfWeek)
    {
        var schedule = await _context.Schedules
            .Where(s => s.DoctorId == idDoctor && s.DayOfWeek == dayOfWeek)
            .ToListAsync();
        _context.Schedules.RemoveRange(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<List<Schedule>> PutSchedule(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime)
    {
        var schedules = await _context.Schedules
            .Where(s => s.DoctorId == idDoctor && s.DayOfWeek == dayOfWeek)
            .ToListAsync();

        if (schedules.Count == 0) throw new NotFoundException("Schedule not found for this date");

        var oldCellTimes = await _context.CellTimes
            .Where(ct => schedules.Select(s => s.Id).Contains(ct.ScheduleId))
            .ToListAsync();
        _context.CellTimes.RemoveRange(oldCellTimes);
        await _context.SaveChangesAsync();

        for (var i = 0; i < 3; i++)
        {
            schedules[i].DayOfWeek = dayOfWeek;
            schedules[i].StartTime = startTime.AddDays(i * 7);
            schedules[i].EndTime = endTime.AddDays(i * 7);
            schedules[i].AppointmentDuration = appointmentDuration;
            schedules[i].StartLunchTime = startLaunchTime.AddDays(i * 7);
            schedules[i].EndLunchTime = endLaunchTime.AddDays(i * 7);
        }

        _context.Schedules.UpdateRange(schedules);
        await _context.SaveChangesAsync();

        foreach (var schedule in schedules)
        {
            var cellTimes = GenerateCellTimes(schedule.StartTime, schedule.EndTime,
                schedule.AppointmentDuration,
                schedule.StartLunchTime, schedule.EndLunchTime, schedule.Id);
            _context.CellTimes.AddRange(cellTimes);
        }

        await _context.SaveChangesAsync();
        return schedules;
    }

    private List<CellTime> GenerateCellTimes(DateTimeOffset startTime, DateTimeOffset endTime,
        TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime, int scheduleId)
    {
        return Enumerable.Range(0, (int)((endTime - startTime).TotalMinutes / appointmentDuration.TotalMinutes))
            .Select(i => startTime.AddMinutes(i * appointmentDuration.TotalMinutes))
            .Where(time => !(time >= startLaunchTime && time < endLaunchTime))
            .Select(time => new CellTime { Time = time.ToUniversalTime(), ScheduleId = scheduleId, PatientId = null })
            .ToList();
    }
}