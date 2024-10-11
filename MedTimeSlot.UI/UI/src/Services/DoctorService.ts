import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Doctor} from "../Models/doctor.model";
import {Observable} from "rxjs";
import {CreateAppointmentModel} from "../Models/createAppointment.model";
import {ScheduleModel} from "../Models/schedule.model";
import {cellTimeModel} from "../Models/celltime.model";



@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  constructor(private http: HttpClient) {}

  getAllDoctors() : Observable<Doctor[]> {
    return this.http.get<Doctor[]>('http://localhost:5080/doctor')
  }

  postCreateAppointment(appointment: CreateAppointmentModel) {
    return this.http.post('http://localhost:5080/Appointment', appointment)
  }

  getScheduleDoctor(doctorId : number) :Observable<ScheduleModel[]> {
    return this.http.get<ScheduleModel[]>('http://localhost:5080/schedule/' + doctorId)
  }

  getCellTimes(doctorId : number, date: string) :Observable<cellTimeModel[]> {
    return this.http.get<cellTimeModel[]>(`http://localhost:5080/Schedule?idDoctor=${doctorId}&Date=${date}`)
  }

  getAppointment(patientId: number ):Observable<cellTimeModel[]> {
    return this.http.get<cellTimeModel[]>(`http://localhost:5080/Appointment/${patientId}`)
  }

}
