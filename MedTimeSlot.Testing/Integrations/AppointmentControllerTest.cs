using System.Text;
using MedTimeSlot.Api.DTOs;
using MedTimeSlot.Api.DTOs.Appointment;
using MedTimeSlot.Api.DTOs.Patient;
using MedTimeSlot.Api.DTOs.Schedule;
using MedTimeSlot.Api.Mapping;
using MedTimeSlot.Application.Services;
using MedTimeSlot.Core.Abstractions;
using MedTimeSlot.Core.Abstractions.Repositories;
using MedTimeSlot.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MedTimeSlot.Testing;

[TestFixture]
public class AppointmentControllerTest
{
    private readonly WebApplicationFactory<Program> _factory;

    public AppointmentControllerTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAppointmentService, AppointmentService>();
                services.AddScoped<IAppointmentRepository, AppointmentRepository>();
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IDoctorRepository, DoctorRepository>();
                services.AddScoped<IPatientService, PatientService>();
                services.AddScoped<IPatientRepository, PatientRepository>();
                services.AddScoped<IScheduleService, ScheduleService>();
                services.AddScoped<IScheduleRepository, ScheduleRepository>();
                services.AddAutoMapper(typeof(MappingProfile));
            });
        });
    }


    [Test]
    public async Task CreateAppointmentTest()
    {
        var client = _factory.CreateClient();

        //Create Doctor
        var doctorRequest = new DoctorRequest("John", "John", "Doctor", "John Doctor", 12);
        var jsonDoctor = JsonConvert.SerializeObject(doctorRequest);
        var contentDoctor = new StringContent(jsonDoctor, Encoding.UTF8, "application/json");
        
        //Create Patient
        var patientRequest = new PatientRequest(1, "John", "John Doctor", "John Doctor", 12);
        var jsonPatient = JsonConvert.SerializeObject(patientRequest);
        var contentPatient = new StringContent(jsonPatient, Encoding.UTF8, "application/json");
        
        //Create Schedule
        var idDoctor = 1;
        var dayOfWeek = DayOfWeek.Tuesday;
        var startTime = new DateTimeOffset(2024, 10, 1, 9, 0, 0, TimeSpan.Zero);
        var endTime = new DateTimeOffset(2024, 10, 1, 19, 0, 0, TimeSpan.Zero);
        var appointmentDuration = TimeSpan.FromMinutes(15);
        var startLaunchTime = new DateTimeOffset(2024, 10, 1, 12, 0, 0, TimeSpan.Zero);
        var endLaunchTime = new DateTimeOffset(2024, 10, 1, 13, 0, 0, TimeSpan.Zero);

        var scheduleRequest = new CreateScheduleRequest(idDoctor, dayOfWeek, startTime, endTime, appointmentDuration,
            startLaunchTime, endLaunchTime);
        var jsonSchedule = JsonConvert.SerializeObject(scheduleRequest);
        var contentSchedule = new StringContent(jsonSchedule, Encoding.UTF8, "application/json");

        //Create Appointment
        var appointmentRequest = new CreateAppointmentRequest(1, 1, startTime);
        var jsonAppointment = JsonConvert.SerializeObject(appointmentRequest);
        var contentAppointment = new StringContent(jsonAppointment, Encoding.UTF8, "application/json");

        //Act
        await client.PostAsync("doctor", contentDoctor);
        await client.PostAsync("patient", contentPatient);
        await client.PostAsync("schedule", contentSchedule);
        var response = await client.PostAsync("appointment", contentAppointment);
        var responseString = await response.Content.ReadAsStringAsync();

        ClassicAssert.AreEqual(responseString, "{\"time\":\"2024-10-01T09:00:00+00:00\"}");
    }
}