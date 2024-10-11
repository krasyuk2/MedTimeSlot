using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;
using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Exceptions;

namespace MedTimeSlot.DataAccess.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly MedicalTimeSlotDbContext _context;

    public PatientRepository(MedicalTimeSlotDbContext context)
    {
        _context = context;
    }


    public async Task<Patient> CreatePatientAsync(int id, string login, string firstName, string lastName, int age)
    {
        var patientCheck = _context.Patients.FirstOrDefault(p => p.Id == id);
        if (patientCheck != null) throw new Exception("Patient already exists");

        var patient = new Patient
        {
            Id = id,
            Login = login,
            Age = age,
            FirstName = firstName,
            LastName = lastName
        };

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient> GetPatientByLoginAsync(string login)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.Login.ToLower() == login.ToLower());
        if (patient == null) throw new NotFoundException("Invalid login");
        return patient;
    }
}