import { KeycloakService } from "keycloak-angular";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class KeycloakServiceApi {
  private authenticated: boolean = false;

  constructor(private readonly keycloak: KeycloakService) {}

  async keycloakRegister() {
    try {
      await this.keycloak.init({

        config: {
          url: 'http://localhost:8080',
          realm: 'TimeSlotRealm',
          clientId: 'timeSlotClient'
        },
        initOptions: {
          onLoad: 'login-required',
          silentCheckSsoRedirectUri: window.location.origin + '/assets/silent-check-sso.html'
        },
      });

      this.authenticated = await this.keycloak.isLoggedIn();
      console.log(this.keycloak.getUserRoles());
      console.log(await this.keycloakGetUserName())
      console.log('Authenticated:', this.authenticated);
    } catch (error) {
      console.error('Keycloak Init Error:', error);
    }
  }

  async keycloakLogin() {
    try {
      await this.keycloak.login();
      this.authenticated = true;
      console.log('Login successful');
    } catch (error) {
      console.error('Login error:', error);
    }
  }
  async keycloakGetUserName() : Promise<string | undefined> {
        return this.keycloak.loadUserProfile().then(profile => {
           return profile.username
      }).catch(error => {
        console.error('Не удалось загрузить профиль пользователя', error);
        return ""
      });
  }

  async keycloakGetRole() : Promise<string[]> {
    return this.keycloak.getUserRoles()
  }

  async keycloakLogout() {
    await this.keycloak.logout();
    this.authenticated = false;
  }

  isAuthenticated(): boolean {
    console.log(this.keycloak.getToken())
    return this.keycloak.isLoggedIn();
  }

  async getToken() {
    return await this.keycloak.getToken();
  }
}
