namespace MedTimeSlot.Api.DTOs;

public record DoctorResponse(int Id, string FirstName, string LastName, string Speciality, int Age);