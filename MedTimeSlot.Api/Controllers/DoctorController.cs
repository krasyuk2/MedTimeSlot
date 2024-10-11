using AutoMapper;
using MedTimeSlot.Api.DTOs;
using MedTimeSlot.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedTimeSlot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly ILogger<DoctorController> _logger;
    private readonly IMapper _mapper;

    public DoctorController(IDoctorService doctorService, IMapper mapper, ILogger<DoctorController> logger,
        IValidationTokenService validationTokenService)
    {
        _doctorService = doctorService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<List<DoctorResponse>> GetAllDoctorsAsync()
    {
        _logger.LogInformation("Getting all doctors");
        var doctors = await _doctorService.GetAllDoctorsAsync();
        var doctorsResponse = _mapper.Map<List<DoctorResponse>>(doctors);
        _logger.LogInformation("Getting all doctors successfully");
        return doctorsResponse;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<DoctorResponse> AddDoctorAsync([FromBody] DoctorRequest doctorRequest)
    {
        _logger.LogInformation("Adding doctor");
        var doctor = await _doctorService.AddDoctor(doctorRequest.Login, doctorRequest.FirstName,
            doctorRequest.LastName, doctorRequest.Speciality, doctorRequest.Age);
        var doctorResponse = _mapper.Map<DoctorResponse>(doctor);
        _logger.LogInformation("Adding doctor successfully");
        return doctorResponse;
    }

    [Authorize(Policy = "Doctor")]
    [HttpGet("{login}")]
    public async Task<DoctorResponse> GetDoctor(string login)
    {
        _logger.LogInformation("Getting doctor");
        var doctor = await _doctorService.GetDoctorByLoginAsync(login);
        var doctorResponse = _mapper.Map<DoctorResponse>(doctor);
        _logger.LogInformation("Getting doctor successfully");
        return doctorResponse;
    }
}