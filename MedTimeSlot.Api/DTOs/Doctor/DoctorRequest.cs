namespace MedTimeSlot.Api.DTOs;

public record DoctorRequest(string Login, string FirstName, string LastName, string Speciality, int Age);