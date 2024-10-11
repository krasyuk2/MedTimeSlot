using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Exceptions;
using MedTimeSlot.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MedTimeSlot.Testing;

[TestFixture]
public class DoctorRepositoryTest
{
    private MedicalTimeSlotDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<MedicalTimeSlotDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MedicalTimeSlotDbContext(options);
    }

    [Test]
    public async Task GetListDoctorsAsync_WhenContainsDoctors()
    {
        //Arrange
        var options = GetInMemoryDbContext();
        var repository = new DoctorRepository(options);
        var login = "Login";
        var firstName = "John";
        var lastName = "Doe";
        var specialty = "Specialty";
        var age = 30;

        //Act
        var doctor = await repository.AddDoctorAsync(login, firstName, lastName, specialty, age);
        var result = await repository.GetDoctorsAsync();

        // Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.AreEqual(result.Count, 1);
        ClassicAssert.AreEqual(result.Capacity, 4);
    }

    [Test]
    public async Task GetListDoctorsAsync_WhenDoesNotContainsDoctors()
    {
        //Arrange
        var options = GetInMemoryDbContext();
        var repository = new DoctorRepository(options);

        // Act & Assert
        var result = Assert.ThrowsAsync<NotFoundException>(async () => await repository.GetDoctorsAsync());
        ClassicAssert.AreEqual(result.Message, "Doctor not found");
    }
}