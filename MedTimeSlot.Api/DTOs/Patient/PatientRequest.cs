namespace MedTimeSlot.Api.DTOs.Patient;

public record PatientRequest(int Id, string Login, string FirstName, string LastName, int Age);