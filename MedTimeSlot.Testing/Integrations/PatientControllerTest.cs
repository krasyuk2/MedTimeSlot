using System.Net;
using System.Text;
using MedTimeSlot.Api.DTOs.Patient;
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
public class PatientControllerTest
{
    private readonly WebApplicationFactory<Program> _factory;

    public PatientControllerTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IPatientService, PatientService>();
                services.AddScoped<IPatientRepository, PatientRepository>();
                services.AddAutoMapper(typeof(MappingProfile));
            });
        });
    }

    [Test]
    public async Task CreatePatientTest()
    {
        var client = _factory.CreateClient();
        var patient = new PatientRequest(1, "John", "test", "testLast", 12);
        var jsonContest = JsonConvert.SerializeObject(patient);
        var content = new StringContent(jsonContest, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("patient", content);
        var responseString = await response.Content.ReadAsStringAsync();
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(responseString,
            "{\"id\":1,\"firstName\":\"test\",\"lastName\":\"testLast\",\"age\":12}");
    }
}