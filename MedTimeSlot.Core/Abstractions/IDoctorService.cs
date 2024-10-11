using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Core.Abstractions;

public interface IDoctorService
{
   /// <summary>
   ///     Retrieves a list of all doctors asynchronously.
   /// </summary>
   /// <returns>A task representing the asynchronous operation, with a list of <see cref="Doctor" /> objects.</returns>
   /// <exception cref="NoDoctorsFoundException">Thrown when no doctors are found.</exception>
   Task<List<Doctor>> GetAllDoctorsAsync();

   /// <summary>
   ///     Adds a new doctor with the specified details asynchronously.
   /// </summary>
   /// <param name="firstName">The first name of the doctor.</param>
   /// <param name="lastName">The last name of the doctor.</param>
   /// <param name="speciality">The speciality of the doctor.</param>
   /// <param name="age">The age of the doctor. Must be a positive integer.</param>
   /// <returns>A task representing the asynchronous operation, with the added <see cref="Doctor" /> object.</returns>
   Task<Doctor> AddDoctor(string login, string firstName, string lastName, string speciality, int age);

   Task<Doctor> GetDoctorByLoginAsync(string login);
}