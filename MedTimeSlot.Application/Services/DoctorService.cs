using MedTimeSlot.Core.Abstractions;
using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorRepository.GetDoctorsAsync();
        if (doctors.Any()) return doctors;
        throw new Exception("No doctors found");
    }

    public async Task<Doctor> AddDoctor(string login, string firstName, string lastName, string speciality, int age)
    {
        var doctor = await _doctorRepository.AddDoctorAsync(login, firstName, lastName, speciality, age);
        return doctor;
    }

    public async Task<Doctor> GetDoctorByLoginAsync(string login)
    {
        var doctor = await _doctorRepository.GetDoctorByLoginAsync(login);
        return doctor;
    }
}