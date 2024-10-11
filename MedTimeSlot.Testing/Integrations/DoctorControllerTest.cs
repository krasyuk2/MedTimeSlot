using System.Net;
using System.Text;
using MedTimeSlot.Api.DTOs;
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

[TestFixture]
public class DoctorControllerTest
{
    private readonly WebApplicationFactory<Program> _factory;

    public DoctorControllerTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IDoctorRepository, DoctorRepository>();
                services.AddAutoMapper(typeof(MappingProfile));
            });
        });
    }

    [Test]
    public async Task GetAllDoctorsTest()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("doctor");
        var responseString = await response.Content.ReadAsStringAsync();
        ClassicAssert.AreEqual(responseString, "{\"error\":\"Not found: Doctor not found\"}");
    }

    [Test]
    public async Task CreateDoctorTest()
    {
        var client = _factory.CreateClient();
        var doctorRequest = new DoctorRequest("John", "Jon", "Doctor", "Car", 31);
        var jsonContent = JsonConvert.SerializeObject(doctorRequest);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/doctor", content);
        var responseString = await response.Content.ReadAsStringAsync();

        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(responseString,
            "{\"id\":1,\"firstName\":\"Jon\",\"lastName\":\"Doctor\",\"speciality\":\"Car\",\"age\":31}");
    }
}