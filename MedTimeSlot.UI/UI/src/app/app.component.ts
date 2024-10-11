import {Component, inject, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { KeycloakAngularModule } from 'keycloak-angular';

import {KeycloakServiceApi} from "../Services/KeycloakServiceApi";
import {KeycloakComponent} from "../Components/keycloak.component";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {AppointmentFormComponent} from "../Components/schedule/schedule.component";
import {DoctorComponent} from "../Components/Doctor/doctor.component";
import {NgIf} from "@angular/common";
import {RouteEntityService} from "../Services/RouteEntityService";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, KeycloakAngularModule, HttpClientModule, KeycloakComponent, AppointmentFormComponent, NgIf, DoctorComponent],
  providers: [KeycloakServiceApi, DoctorComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'UI';
  username :string | undefined = ""
  role : string[] = []
  id: number
  isDoctor: boolean = false

  constructor(private keycloakServiceApi: KeycloakServiceApi, private routeEntity : RouteEntityService) {}

  async ngOnInit() {
    await this.keycloakServiceApi.keycloakRegister();
    this.username = await this.keycloakServiceApi.keycloakGetUserName()
    this.role = await this.keycloakServiceApi.keycloakGetRole();
    this.isDoctor = this.role.includes("Doctor")
    this.routeEntity.isDoctor = this.isDoctor
    if(this.isDoctor){
      this.routeEntity.getDoctorApi(this.username).subscribe( doctor => {
        this.routeEntity.setDoctor(doctor);
      })
    }
    else {
      this.routeEntity.getPatientApi(this.username).subscribe(patient => {
        this.routeEntity.setPatient(patient);
        console.log(patient)
      })
    }
  }
  http = inject(HttpClient)







}
