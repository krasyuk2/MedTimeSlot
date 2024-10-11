using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.Core.Models;
using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MedTimeSlot.DataAccess.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly MedicalTimeSlotDbContext _context;

    public DoctorRepository(MedicalTimeSlotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        var doctors = await _context.Doctors.ToListAsync();
        if (!doctors.Any()) throw new NotFoundException("Doctor not found");
        return doctors;
    }

    public async Task<Doctor> AddDoctorAsync(string login, string firstName, string lastName, string speciality,
        int age)
    {
        var doctor = new Doctor
        {
            Login = login,
            FirstName = firstName,
            LastName = lastName,
            Speciality = speciality,
            Age = age
        };
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor> GetDoctorByLoginAsync(string login)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
        if (doctor == null) throw new NotFoundException("Doctor not found");
        return doctor;
    }
}