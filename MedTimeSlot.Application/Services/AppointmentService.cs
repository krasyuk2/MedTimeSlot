using MedTimeSlot.Core.Abstractions;
using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<CellTime> CreateAppointment(int idPatient, int idDoctor, DateTimeOffset appointmentDate)
    {
        return await _appointmentRepository.CreateAppointment(idPatient, idDoctor, appointmentDate);
    }

    public async Task<List<CellTime>> GetAppointmentsForPatient(int idPatient)
    {
        return await _appointmentRepository.GetAppointmentsForPatient(idPatient);
    }

    public async Task<CellTime> RemoveAppointment(int idPatient, DateTimeOffset appointmentDate)
    {
        return await _appointmentRepository.RemoveAppointment(idPatient, appointmentDate);
    }
}