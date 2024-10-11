using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions.Repositories;

public interface IPatientRepository
{
    Task<Patient> CreatePatientAsync(int id, string login, string firstName, string lastName, int age);
    Task<Patient> GetPatientByLoginAsync(string login);
}