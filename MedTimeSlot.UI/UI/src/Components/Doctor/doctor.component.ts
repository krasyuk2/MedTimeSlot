import {Component, OnInit} from "@angular/core";
import {HttpClientModule} from "@angular/common/http";
import {DoctorService} from "../../Services/DoctorService";
import {Doctor} from "../../Models/doctor.model";
import {NgFor, NgIf} from "@angular/common";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";

import {CreateAppointmentModel} from "../../Models/createAppointment.model";

import {ScheduleModel} from "../../Models/schedule.model";
import {cellTimeModel} from "../../Models/celltime.model";
import {RouteEntityService} from "../../Services/RouteEntityService";
import {Patient} from "../../Models/patient.model";




@Component({
  selector: 'app-doctor',
  standalone: true,
  templateUrl: "./doctor.component.html",
  styleUrls: ['./doctor.component.css'],
  imports: [HttpClientModule, NgFor, NgIf, FormsModule, ReactiveFormsModule]
})
export class DoctorComponent implements OnInit {
  doctors: Doctor[] =[]
  doctorId: number;
  schedules: ScheduleModel[] = []
  cellTimes: cellTimeModel[] = []
  appointment: cellTimeModel[] = []
  day: string
  selectedCellTime: string | null = null;


  constructor(private doctorService: DoctorService, private routeService: RouteEntityService) {}

  ngOnInit() {
    this.getAllDoctors()
  }
   toISOStringWithOffset  (localDateString: string): string {
    const localDate = new Date(localDateString);
    const timezoneOffset = localDate.getTimezoneOffset() * 60000; // В миллисекундах
    const utcDate = new Date(localDate.getTime() - timezoneOffset);
    return utcDate.toISOString();
  };

  getAllDoctors() {
    this.doctorService.getAllDoctors().subscribe(
      (data : Doctor[]) => {
        console.log(data);
        this.doctors = data
      },
      (error) => {
        console.error('Error fetching doctors:', error);
      }
    );
  }
  onDoctorClick(doctor: Doctor) {
    console.log('Clicked doctor:', doctor);
  }

  getSchedules(doctorId: number) {
    this.doctorService.getScheduleDoctor(doctorId).subscribe((cellTimes : ScheduleModel[]) => {
      this.schedules = cellTimes;
      this.doctorId = doctorId
      console.log(this.schedules);
    })
  }

  getCellTimes(date: string) {
    this.doctorService.getCellTimes(this.doctorId, this.toISOStringWithOffset(date)).subscribe((cellTimes : cellTimeModel[]) => {
      this.cellTimes = cellTimes;
    })
  }


  createAppointment(cellTime: string) {

    const appointment: CreateAppointmentModel = {
      idDoctor: this.doctorId,
      idPatient: this.routeService.patient.id,
      appointmentDate: cellTime,
    };
    console.log(appointment);
    this.doctorService.postCreateAppointment(appointment).subscribe((date) => {
      console.log(date);
      this.selectedCellTime = cellTime; // Устанавливаем выбранное время
    });
    this.getAppointment();
    this.getCellTimes(cellTime)
  }

  getAppointment() {
    this.doctorService.getAppointment(this.routeService.patient.id).subscribe((cellTime: cellTimeModel[]) => {
      this.appointment = cellTime;
    })
  }

}
