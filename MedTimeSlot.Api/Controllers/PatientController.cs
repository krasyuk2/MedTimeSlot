using AutoMapper;
using MedTimeSlot.Api.DTOs.Patient;
using MedTimeSlot.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedTimeSlot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : Controller
{
    private readonly ILogger<PatientController> _logger;
    private readonly IMapper _mapper;
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService, IMapper mapper, ILogger<PatientController> logger)
    {
        _patientService = patientService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<PatientResponse> CreatePatient([FromBody] PatientRequest request)
    {
        _logger.LogInformation("Creating patient");
        var patient =
            await _patientService.CreatePatientAsync(request.Id, request.Login, request.FirstName, request.LastName,
                request.Age);
        var patientResponse = _mapper.Map<PatientResponse>(patient);
        _logger.LogInformation("Created patient");
        return patientResponse;
    }

    [Authorize(Policy = "Patient")]
    [HttpGet("{login}")]
    public async Task<PatientResponse> GetPatientByLoginAsync(string login)
    {
        _logger.LogInformation("Getting patient by login");
        var patient = await _patientService.GetPatientByLoginAsync(login);
        var patientResponse = _mapper.Map<PatientResponse>(patient);
        _logger.LogInformation("Retrieved patient");
        return patientResponse;
    }
}