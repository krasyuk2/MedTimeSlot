import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {CreateSchedule} from "../Models/crateBaseSchedule.model";




@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  constructor(private http: HttpClient) {}

  crateBaseSchedule(appointment: CreateSchedule) {
    return this.http.post('http://localhost:5080/Schedule', appointment)
  }

}
