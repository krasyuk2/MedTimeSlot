import {Component} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';

import {HttpClient, HttpClientModule} from '@angular/common/http';
import {CreateSchedule} from "../../Models/crateBaseSchedule.model";
import {ScheduleService} from "../../Services/ScheduleService";
import {RouteEntityService} from "../../Services/RouteEntityService";


@Component({
  selector: 'app-schedule',
  standalone: true,
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.css',
  imports: [HttpClientModule, ReactiveFormsModule, FormsModule,]
})
export class AppointmentFormComponent {
  appointmentForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, private scheduleService: ScheduleService, private routeService: RouteEntityService) {
    this.appointmentForm = this.fb.group({

      DayOfWeek: ['', Validators.required],
      StartTime: ['', Validators.required],
      EndTime: ['', Validators.required],
      AppointmentDuration: ['', Validators.required],
      StartLaunchTime: ['', Validators.required],
      EndLaunchTime: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.appointmentForm.valid) {
      const {
        DayOfWeek,
        StartTime,
        EndTime,
        AppointmentDuration,
        StartLaunchTime,
        EndLaunchTime
      } = this.appointmentForm.value;


      const toISOStringWithOffset = (localDateString: string): string => {
        const localDate = new Date(localDateString);
        const timezoneOffset = localDate.getTimezoneOffset() * 60000; // В миллисекундах
        const utcDate = new Date(localDate.getTime() - timezoneOffset);
        return utcDate.toISOString();
      };

      const appointment: CreateSchedule = {
        IdDoctor: this.routeService.doctor.id,
        DayOfWeek: DayOfWeek,
        StartTime: toISOStringWithOffset(StartTime),
        EndTime: toISOStringWithOffset(EndTime),
        AppointmentDuration: AppointmentDuration,
        StartLaunchTime: toISOStringWithOffset(StartLaunchTime),
        EndLaunchTime: toISOStringWithOffset(EndLaunchTime)
      };

      this.crateBaseSchedule(appointment);
    }
  }

  crateBaseSchedule(appointment: CreateSchedule) {
    this.scheduleService.crateBaseSchedule(appointment)
      .subscribe(response => {
        console.log('Appointment submitted successfully!', response);
      }, error => {
        console.error('Error submitting appointment', error);
      });
  }


}
