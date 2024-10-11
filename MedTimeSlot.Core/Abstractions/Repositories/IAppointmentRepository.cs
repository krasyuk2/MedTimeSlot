using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions.Repositories;

public interface IAppointmentRepository
{
    Task<CellTime> CreateAppointment(int idPatient, int idDoctor, DateTimeOffset appointmentDate);
    Task<List<CellTime>> GetAppointmentsForPatient(int idPatient);
    Task<CellTime> RemoveAppointment(int idPatient, DateTimeOffset appointmentDate);
}