using MedTimeSlot.Core.Abstractions;
using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Patient> CreatePatientAsync(int id, string login, string firstName, string lastName, int age)
    {
        return await _patientRepository.CreatePatientAsync(id, login, firstName, lastName, age);
    }

    public async Task<Patient> GetPatientByLoginAsync(string login)
    {
        return await _patientRepository.GetPatientByLoginAsync(login);
    }
}