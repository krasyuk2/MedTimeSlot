using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions;

public interface IScheduleService
{
    /// <summary>
    ///     Creates base schedules for a specific doctor on a specified day of the week.
    /// </summary>
    /// <param name="idDoctor">The ID of the doctor.</param>
    /// <param name="dayOfWeek">The day of the week for the schedule.</param>
    /// <param name="startTime">The start time of the schedule.</param>
    /// <param name="endTime">The end time of the schedule.</param>
    /// <param name="appointmentDuration">The duration of each appointment.</param>
    /// <param name="startLaunchTime">The start time for lunch breaks.</param>
    /// <param name="endLaunchTime">The end time for lunch breaks.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="CellTime" /> objects.</returns>
    Task<List<CellTime>> CreateBaseSchedules(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime);

    /// <summary>
    ///     Retrieves all schedules for a specific doctor.
    /// </summary>
    /// <param name="doctorId">The unique identifier of the doctor.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of <see cref="Schedule" />
    ///     objects.
    /// </returns>
    Task<List<Schedule>> GetAllSchedules(int doctorId);

    /// <summary>
    ///     Retrieves the cell times available for a specific doctor on a given date.
    /// </summary>
    /// <param name="doctorId">The unique identifier of the doctor.</param>
    /// <param name="date">The date for which to retrieve the cell times.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of <see cref="CellTime" />
    ///     objects.
    /// </returns>
    Task<List<CellTime>> GetCellTimesByDate(int doctorId, DateTimeOffset date);

    /// <summary>
    ///     Updates or creates a schedule for a specific doctor on a specified day of the week.
    /// </summary>
    /// <param name="idDoctor">The unique identifier of the doctor.</param>
    /// <param name="dayOfWeek">The day of the week for which the schedule is set.</param>
    /// <param name="startTime">The start time of the schedule.</param>
    /// <param name="endTime">The end time of the schedule.</param>
    /// <param name="appointmentDuration">The duration of each appointment.</param>
    /// <param name="startLaunchTime">The start time for lunch breaks.</param>
    /// <param name="endLaunchTime">The end time for lunch breaks.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of <see cref="Schedule" />
    ///     objects after the update.
    /// </returns>
    Task<List<Schedule>> PutSchedule(int idDoctor, DayOfWeek dayOfWeek, DateTimeOffset startTime,
        DateTimeOffset endTime, TimeSpan appointmentDuration, DateTimeOffset startLaunchTime,
        DateTimeOffset endLaunchTime);

    /// <summary>
    ///     Removes a schedule for a specific doctor on a specified day of the week.
    /// </summary>
    /// <param name="idDoctor">The unique identifier of the doctor.</param>
    /// <param name="dayOfWeek">The day of the week for which the schedule will be removed.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of <see cref="Schedule" />
    ///     objects after the removal.
    /// </returns>
    Task<List<Schedule>> RemoveSchedule(int idDoctor, DayOfWeek dayOfWeek);
}