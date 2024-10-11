using MedTimeSlot.DataAccess.DataAccess;
using MedTimeSlot.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MedTimeSlot.Testing;

[TestFixture]
public class PatientRepositoryTest
{
    private MedicalTimeSlotDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<MedicalTimeSlotDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MedicalTimeSlotDbContext(options);
    }

    [Test]
    public async Task CreatePatientAsync_ShouldAddPatient_WhenPatientDoesNotExist()
    {
        // Arrange 
        var options = GetInMemoryDbContext();
        var repository = new PatientRepository(options);
        var id = 1;
        var firstName = "John";
        var lastName = "Doe";
        var login = "John";
        var age = 30;

        // Act
        var patient = await repository.CreatePatientAsync(id, login, firstName, lastName, age);

        // Assert
        ClassicAssert.AreEqual(patient.Id, id);
    }

    [Test]
    public async Task CreatePatientAsync_ShouldThrowException_WhenPatientAlreadyExists()
    {
        // Arrange
        var options = GetInMemoryDbContext();
        var repository = new PatientRepository(options);
        var id = 1;
        var firstName = "John";
        var login = "John";
        var lastName = "Doe";
        var age = 30;

        // Создаем первого пациента
        await repository.CreatePatientAsync(id, login, firstName, lastName, age);

        // Act & Assert
        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await repository.CreatePatientAsync(id, "Lo", "Jane", "Doe", 25));
        ClassicAssert.AreEqual("Patient already exists", exception.Message); // Проверка сообщения исключения
    }
}