using AutoMapper;
using MedTimeSlot.Api.DTOs.Appointment;
using MedTimeSlot.Api.DTOs.CellTime;
using MedTimeSlot.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedTimeSlot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentController> _logger;
    private readonly IMapper _mapper;

    public AppointmentController(IAppointmentService appointmentService, IMapper mapper,
        ILogger<AppointmentController> logger)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Policy = "Patient")]
    public async Task<CellTImeResponse> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        _logger.LogInformation("Creating new appointment");
        var appointment =
            await _appointmentService.CreateAppointment(request.IdPatient, request.IdDoctor, request.AppointmentDate);
        var appointmentResponse = _mapper.Map<CellTImeResponse>(appointment);
        return appointmentResponse;
    }

    [Authorize(Policy = "Patient")]
    [HttpGet("{idPatient}")]
    public async Task<List<CellTImeResponse>> GetAppointmentsByPatient([FromRoute] int idPatient)
    {
        _logger.LogInformation("Getting appointments by Patient");
        var cellTimes = await _appointmentService.GetAppointmentsForPatient(idPatient);
        var cellTimesResponse = _mapper.Map<List<CellTImeResponse>>(cellTimes);
        return cellTimesResponse;
    }

    [Authorize(Policy = "Patient")]
    [HttpDelete]
    public async Task<CellTImeResponse> RemoveAppointment([FromBody] RemoveAppointmentRequest request)
    {
        _logger.LogInformation("Removing appointment");
        var cellTimes = await _appointmentService.RemoveAppointment(request.IdPatient, request.AppointmentDate);
        var cellTimesResponse = _mapper.Map<CellTImeResponse>(cellTimes);
        return cellTimesResponse;
    }
}