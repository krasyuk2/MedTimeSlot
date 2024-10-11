namespace MedTimeSlot.Api.DTOs.Appointment;

public record RemoveAppointmentRequest(int IdPatient, DateTimeOffset AppointmentDate);