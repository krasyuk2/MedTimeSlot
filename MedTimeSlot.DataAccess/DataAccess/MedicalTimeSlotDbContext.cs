using MedTimeSlot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MedTimeSlot.DataAccess.DataAccess;

public class MedicalTimeSlotDbContext : DbContext
{
    public MedicalTimeSlotDbContext(DbContextOptions<MedicalTimeSlotDbContext> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<CellTime> CellTimes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1, Login = "Doctor1", FirstName = "Tom", LastName = "A", Speciality = "Therapist", Age = 17
            },
            new Doctor
            {
                Id = 2, Login = "Doctor2", FirstName = "Bob", LastName = "B", Speciality = "Traumatologist", Age = 23
            },
            new Doctor
            {
                Id = 3, Login = "Doctor3", FirstName = "Sam", LastName = "C", Speciality = "Surgeon", Age = 42
            }
        );
        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id = 1, Login = "Patient1", FirstName = "Tom", LastName = "A", Age = 29 },
            new Patient { Id = 2, Login = "Patient2", FirstName = "Bob", LastName = "B", Age = 29 },
            new Patient { Id = 3, Login = "Patient3", FirstName = "Sam", LastName = "C", Age = 29 }
        );
    }
}