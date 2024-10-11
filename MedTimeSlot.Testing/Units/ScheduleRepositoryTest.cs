using MedTimeSlot.Core.Models;
using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MedTimeSlot.Testing;

[TestFixture]
public class ScheduleRepositoryTest
{
    private MedicalTimeSlotDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<MedicalTimeSlotDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MedicalTimeSlotDbContext(options);
    }


    [Test]
    public async Task CreateBaseSchedulesTest()
    {
        // Arrange
        var options = GetInMemoryDbContext();
        await options.Doctors.AddAsync(
            new Doctor { Id = 1, FirstName = "k", LastName = "l", Speciality = "T", Age = 1 });
        await options.SaveChangesAsync();
        var scheduleRepository = new ScheduleRepository(options);

        var idDoctor = 1;
        var dayOfWeek = DayOfWeek.Tuesday;
        var startTime = new DateTimeOffset(2024, 10, 1, 9, 0, 0, TimeSpan.Zero);
        var endTime = new DateTimeOffset(2024, 10, 1, 19, 0, 0, TimeSpan.Zero);
        var appointmentDuration = TimeSpan.FromMinutes(15);
        var startLaunchTime = new DateTimeOffset(2024, 10, 1, 12, 0, 0, TimeSpan.Zero);
        var endLaunchTime = new DateTimeOffset(2024, 10, 1, 13, 0, 0, TimeSpan.Zero);

        //Act
        var result = await scheduleRepository.CreateBaseSchedules(idDoctor, dayOfWeek, startTime, endTime,
            appointmentDuration, startLaunchTime, endLaunchTime);

        //Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.AreEqual(result[0].Time, new DateTimeOffset(2024, 10, 1, 9, 0, 0,
            TimeSpan.Zero));
        ClassicAssert.AreEqual(result[1].Time, new DateTimeOffset(2024, 10, 1, 9, 15, 0,
            TimeSpan.Zero));
        ClassicAssert.AreEqual(result[0].PatientId, null);
    }

    [Test]
    public async Task GetAllSchedulesTest()
    {
        // Arrange
        var options = GetInMemoryDbContext();

        await new DoctorRepository(options).AddDoctorAsync("f", "k", "l", "T", 1);
        var scheduleRepository = new ScheduleRepository(options);

        var idDoctor = 1;
        var dayOfWeek = DayOfWeek.Tuesday;
        var startTime = new DateTimeOffset(2024, 10, 1, 9, 0, 0, TimeSpan.Zero);
        var endTime = new DateTimeOffset(2024, 10, 1, 19, 0, 0, TimeSpan.Zero);
        var appointmentDuration = TimeSpan.FromMinutes(15);
        var startLaunchTime = new DateTimeOffset(2024, 10, 1, 12, 0, 0, TimeSpan.Zero);
        var endLaunchTime = new DateTimeOffset(2024, 10, 1, 13, 0, 0, TimeSpan.Zero);

        await scheduleRepository.CreateBaseSchedules(idDoctor, dayOfWeek, startTime, endTime,
            appointmentDuration, startLaunchTime, endLaunchTime);
        //Act
        var result = await scheduleRepository.GetAllSchedules(1);

        //Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.AreEqual(result.Count, 3);
        ClassicAssert.AreEqual(result[0].Times.First().Time, new DateTimeOffset(2024, 10, 1, 9,
            0, 0, TimeSpan.Zero));
    }
}