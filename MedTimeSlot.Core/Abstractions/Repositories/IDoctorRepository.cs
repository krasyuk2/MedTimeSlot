using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions.Repositories;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetDoctorsAsync();
    Task<Doctor> AddDoctorAsync(string login, string firstName, string lastName, string speciality, int age);
    Task<Doctor> GetDoctorByLoginAsync(string login);
}