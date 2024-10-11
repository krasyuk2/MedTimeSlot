using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions;

public interface IAppointmentService
{
    /// <summary>
    ///     Creates a new appointment for a specified patient with a specified doctor at a given date and time.
    /// </summary>
    /// <param name="idPatient">The ID of the patient who is creating the appointment.</param>
    /// <param name="idDoctor">The ID of the doctor with whom the appointment is being scheduled.</param>
    /// <param name="appointmentDate">The date and time of the appointment.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, with a <see cref="CellTime" /> object representing the created
    ///     appointment.
    /// </returns>
    Task<CellTime> CreateAppointment(int idPatient, int idDoctor, DateTimeOffset appointmentDate);

    /// <summary>
    ///     Retrieves a list of appointments for a specified patient.
    /// </summary>
    /// <param name="idPatient">The ID of the patient whose appointments are to be retrieved.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, with a list of <see cref="CellTime" /> objects representing
    ///     the patient's appointments.
    /// </returns>
    Task<List<CellTime>> GetAppointmentsForPatient(int idPatient);

    /// <summary>
    ///     Removes an appointment for a specified patient at a given date and time.
    /// </summary>
    /// <param name="idPatient">The ID of the patient who is removing the appointment.</param>
    /// <param name="appointmentDate">The date and time of the appointment to be removed.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, with a <see cref="CellTime" /> object representing the removed
    ///     appointment.
    /// </returns>
    Task<CellTime> RemoveAppointment(int idPatient, DateTimeOffset appointmentDate);
}