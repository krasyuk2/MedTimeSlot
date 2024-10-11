using System.Text;
using MedTimeSlot.Api.DTOs;
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

namespace MedTimeSlot.Testing.Integrations;

[TestFixture]
public class ScheduleControllerTest
{
    private readonly WebApplicationFactory<Program> _factory;

    public ScheduleControllerTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IScheduleService, ScheduleService>();
                services.AddScoped<IScheduleRepository, ScheduleRepository>();
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IDoctorRepository, DoctorRepository>();
                services.AddAutoMapper(typeof(MappingProfile));
            });
        });
    }

    [Test]
    public async Task CreateScheduleTest_DoctorNotFound()
    {
        var client = _factory.CreateClient();

        var idDoctor = 1;
        var dayOfWeek = DayOfWeek.Tuesday;
        var startTime = new DateTimeOffset(2024, 10, 1, 9, 0, 0, TimeSpan.Zero);
        var endTime = new DateTimeOffset(2024, 10, 1, 19, 0, 0, TimeSpan.Zero);
        var appointmentDuration = TimeSpan.FromMinutes(15);
        var startLaunchTime = new DateTimeOffset(2024, 10, 1, 12, 0, 0, TimeSpan.Zero);
        var endLaunchTime = new DateTimeOffset(2024, 10, 1, 13, 0, 0, TimeSpan.Zero);

        var scheduleRequest = new CreateScheduleRequest(idDoctor, dayOfWeek, startTime, endTime, appointmentDuration,
            startLaunchTime, endLaunchTime);
        var json = JsonConvert.SerializeObject(scheduleRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("schedule", content);
        var responseString = await response.Content.ReadAsStringAsync();

        ClassicAssert.AreEqual(responseString, "{\"error\":\"Not found: Doctor not found\"}");
    }

    [Test]
    public async Task CreateScheduleTest_OK()
    {
        var client = _factory.CreateClient();

        var idDoctor = 1;
        var dayOfWeek = DayOfWeek.Tuesday;
        var startTime = new DateTimeOffset(2024, 10, 1, 9, 0, 0, TimeSpan.Zero);
        var endTime = new DateTimeOffset(2024, 10, 1, 19, 0, 0, TimeSpan.Zero);
        var appointmentDuration = TimeSpan.FromMinutes(15);
        var startLaunchTime = new DateTimeOffset(2024, 10, 1, 12, 0, 0, TimeSpan.Zero);
        var endLaunchTime = new DateTimeOffset(2024, 10, 1, 13, 0, 0, TimeSpan.Zero);


        var doctorRequest = new DoctorRequest("John", "Jon", "Doctor", "Car", 31);
        var jsonContent = JsonConvert.SerializeObject(doctorRequest);
        var contentDoctor = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        await client.PostAsync("/doctor", contentDoctor);

        var scheduleRequest = new CreateScheduleRequest(idDoctor, dayOfWeek, startTime, endTime, appointmentDuration,
            startLaunchTime, endLaunchTime);
        var json = JsonConvert.SerializeObject(scheduleRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await client.PostAsync("schedule", content);
        var responseString = await response.Content.ReadAsStringAsync();

        ClassicAssert.IsNotNull(responseString);
    }
}