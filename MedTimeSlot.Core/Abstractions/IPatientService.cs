using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions;

public interface IPatientService
{
    /// <summary>
    ///     Creates a new patient asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier for the patient.</param>
    /// <param name="firstName">The first name of the patient.</param>
    /// <param name="lastName">The last name of the patient.</param>
    /// <param name="age">The age of the patient.</param>
    /// <returns>A task representing the asynchronous operation, with the created <see cref="Patient" /> object.</returns>
    Task<Patient> CreatePatientAsync(int id, string login, string firstName, string lastName, int age);

    Task<Patient> GetPatientByLoginAsync(string login);
}