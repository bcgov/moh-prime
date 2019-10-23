import { Injectable } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';

import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private keycloakService: KeycloakService
  ) { }

  public async getUser(forceReload?: boolean): Promise<User> {
    const {
      username: userId,
      firstName,
      lastName,
      email: contactEmail
    } = await this.keycloakService.loadUserProfile(forceReload);

    return {
      userId,
      firstName,
      lastName,
      contactEmail
    };
  }

  public getRoles(): string[] {
    return this.keycloakService.getUserRoles();
  }

  public hasRole(role: string): boolean {
    return this.keycloakService.isUserInRole(role);
  }

  // TODO: what is the applicant role?
  public isApplicant(): boolean {
    return this.hasRole('');
  }

  // TODO: what is the provisioner role?
  public isProvisioner(): boolean {
    return this.hasRole('');
  }

  // TODO: what is the admin role?
  public isAdmin(): boolean {
    return this.hasRole('');
  }

  public isTokenExpired(): boolean {
    return this.keycloakService.isTokenExpired();
  }

  public removeToken() {
    this.keycloakService.clearToken();
  }

  public isLoggedIn(): Promise<boolean> {
    return this.keycloakService.isLoggedIn();
  }

  public logout(redirectUri?: string): Promise<void> {
    return this.keycloakService.logout(redirectUri);
  }
}
