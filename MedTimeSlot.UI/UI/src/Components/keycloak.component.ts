import {Component, OnInit} from "@angular/core";
import {KeycloakServiceApi} from "../Services/KeycloakServiceApi";


@Component({
  selector: "keycloak-app",
  standalone: true,
  template: `<button (click)="logout()">Выйти</button>/`,
  providers: [KeycloakServiceApi]
})
export class KeycloakComponent implements OnInit {
  constructor(private keycloakServiceApi: KeycloakServiceApi) {}

  ngOnInit() {
    const isAuthenticated = this.keycloakServiceApi.isAuthenticated();
    console.log('Is user authenticated?', isAuthenticated);
  }

  async logout() {
    await this.keycloakServiceApi.keycloakLogout()
  }
}
