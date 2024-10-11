namespace MedTimeSlot.Api.DTOs.Appointment;

public record CreateAppointmentRequest(int IdPatient, int IdDoctor, DateTimeOffset AppointmentDate);