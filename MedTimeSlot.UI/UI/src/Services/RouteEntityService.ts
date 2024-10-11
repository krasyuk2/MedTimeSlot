import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Doctor} from "../Models/doctor.model";
import {BehaviorSubject, Observable} from "rxjs";
import {Patient} from "../Models/patient.model";


@Injectable({
  providedIn: 'root'
})
export class RouteEntityService  {


  constructor(private http: HttpClient) {}

  isDoctor : boolean = false;
  doctor : Doctor
  patient : Patient
  username: string

  getDoctorApi(login: string  | undefined): Observable<Doctor> {
    return this.http.get<Doctor>('http://localhost:5080/doctor/' + login)
  }

  getPatientApi(login: string  | undefined): Observable<Patient> {
    return this.http.get<Patient>('http://localhost:5080/patient/' + login)
  }

  setDoctor(Doctor : Doctor) {
    this.doctor = Doctor;
  }
  setPatient(Patient : Patient) {
    this.patient = Patient;
  }

  getUsername() : string {
    return this.username;
  }


}
