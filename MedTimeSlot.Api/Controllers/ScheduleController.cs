using AutoMapper;
using MedTimeSlot.Api.DTOs.CellTime;
using MedTimeSlot.Api.DTOs.Schedule;
using MedTimeSlot.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedTimeSlot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : Controller
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly IMapper _mapper;
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService, IMapper mapper, ILogger<ScheduleController> logger)
    {
        _scheduleService = scheduleService;
        _mapper = mapper;
        _logger = logger;
    }

    [Authorize(Policy = "Doctor")]
    [HttpPost]
    public async Task<List<CellTImeResponse>> CreateBaseSchedule([FromBody] CreateScheduleRequest request)
    {
        _logger.LogInformation("Creating schedule");
        var schedules = await _scheduleService.CreateBaseSchedules(request.IdDoctor, request.DayOfWeek,
            request.StartTime, request.EndTime, request.AppointmentDuration,
            request.StartLaunchTime, request.EndLaunchTime);
        var scheduleResponse = _mapper.Map<List<CellTImeResponse>>(schedules);
        _logger.LogInformation("Created schedule");
        return scheduleResponse;
    }


    [HttpGet("{idDoctor}")]
    public async Task<List<ScheduleResponse>> GetAllSchedules([FromRoute] int idDoctor)
    {
        _logger.LogInformation("Getting all schedules");
        var schedules = await _scheduleService.GetAllSchedules(idDoctor);
        var scheduleResponse = _mapper.Map<List<ScheduleResponse>>(schedules);
        _logger.LogInformation("Retrieved all schedules");
        return scheduleResponse;
    }


    [HttpGet]
    public async Task<List<CellTImeResponse>> GetCellTimeByDate([FromQuery] GetScheduleRequest request)
    {
        _logger.LogInformation("Getting cell time by date");
        var cellTimes = await _scheduleService.GetCellTimesByDate(request.IdDoctor, request.Date);
        var scheduleResponse = _mapper.Map<List<CellTImeResponse>>(cellTimes);
        _logger.LogInformation("Retrieved cell time by date");
        return scheduleResponse;
    }

    [Authorize(Policy = "Doctor")]
    [HttpPut]
    public async Task<ScheduleResponse> PutSchedules([FromBody] CreateScheduleRequest request)
    {
        _logger.LogInformation("Updating schedule");
        var schedules = await _scheduleService.PutSchedule(request.IdDoctor, request.DayOfWeek,
            request.StartTime, request.EndTime, request.AppointmentDuration,
            request.StartLaunchTime, request.EndLaunchTime);
        var scheduleResponse = _mapper.Map<ScheduleResponse>(schedules);
        _logger.LogInformation("Updated schedule");
        return scheduleResponse;
    }

    [Authorize(Policy = "Doctor")]
    [HttpDelete]
    public async Task<List<ScheduleResponse>> RemoveSchedule([FromBody] RemoveScheduleRequest request)
    {
        _logger.LogInformation("Removing schedule");
        var schedules = await _scheduleService.RemoveSchedule(request.IdDoctor, request.DayOfWeek);
        var scheduleResponse = _mapper.Map<List<ScheduleResponse>>(schedules);
        _logger.LogInformation("Removed schedule");
        return scheduleResponse;
    }
}