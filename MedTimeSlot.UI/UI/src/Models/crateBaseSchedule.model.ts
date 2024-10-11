export interface CreateSchedule {
  IdDoctor: number;
  DayOfWeek: number;
  StartTime: string;
  EndTime: string;
  AppointmentDuration: string;
  StartLaunchTime: string;
  EndLaunchTime: string;
}
