using AutoMapper;
using MedTimeSlot.Api.DTOs;
using MedTimeSlot.Api.DTOs.CellTime;
using MedTimeSlot.Api.DTOs.Patient;
using MedTimeSlot.Api.DTOs.Schedule;
using MedTimeSlot.Core.Models;

namespace MedTimeSlot.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Doctor, DoctorResponse>();
        CreateMap<Schedule, ScheduleResponse>();
        CreateMap<CellTime, ScheduleResponse>();
        CreateMap<Patient, PatientResponse>();
        CreateMap<CellTime, CellTImeResponse>();
    }
}