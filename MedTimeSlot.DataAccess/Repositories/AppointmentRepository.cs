using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;
using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MedTimeSlot.DataAccess.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly MedicalTimeSlotDbContext _context;

    public AppointmentRepository(MedicalTimeSlotDbContext context)
    {
        _context = context;
    }

    public async Task<CellTime> CreateAppointment(int idPatient, int idDoctor, DateTimeOffset appointmentDate)
    {
        var patient = await _context.Patients.FindAsync(idPatient);
        var doctor = await _context.Doctors.FindAsync(idDoctor);

        if (patient is null || doctor is null) throw new NotFoundException("Patient or Doctor not found");

        var schedules = _context.Schedules.Where(s => s.DoctorId == idDoctor);
        if (schedules == null) throw new NotFoundException("Schedule not found");

        CellTime? cellTime;
        await using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            cellTime = _context.CellTimes
                .FirstOrDefault(i =>
                    i.ScheduleId == schedules.First().Id && i.Time == appointmentDate && i.PatientId == null);

            if (cellTime == null) throw new NotFoundException("Appointment not found");

            cellTime.PatientId = patient.Id;

            _context.CellTimes.Update(cellTime);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        return cellTime;
    }

    public async Task<List<CellTime>> GetAppointmentsForPatient(int idPatient)
    {
        var patient = await _context.Patients.FindAsync(idPatient);
        if (patient is null) throw new NotFoundException("Patient not found");

        var cellTimes = await _context.CellTimes
            .Where(i => i.PatientId == patient.Id)
            .ToListAsync();
        if (cellTimes is null) throw new NotFoundException("Appointment not found");
        return cellTimes;
    }

    public async Task<CellTime> RemoveAppointment(int idPatient, DateTimeOffset appointmentDate)
    {
        var patient = await _context.Patients.FindAsync(idPatient);
        if (patient is null) throw new NotFoundException("Patient not found");

        var cellTime =
            await _context.CellTimes.FirstOrDefaultAsync(i => i.PatientId == patient.Id && i.Time == appointmentDate);
        if (cellTime is null) throw new NotFoundException("Appointment not found");
        cellTime.PatientId = null;
        _context.Update(cellTime);
        await _context.SaveChangesAsync();
        return cellTime;
    }
}