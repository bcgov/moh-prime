import { Injectable } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';
import { JwtHelperService } from '@auth0/angular-jwt';

import { User } from '../models/user.model';
import { LoggerService } from '@core/services/logger.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private jwtHelper: JwtHelperService;

  constructor(
    private logger: LoggerService,
    private keycloakService: KeycloakService
  ) {
    this.jwtHelper = new JwtHelperService();
  }

  public async getUserId(): Promise<string> {
    const token = await this.decodeToken();

    this.logger.info('TOKEN', token);

    return token.sub;
  }

  public async getUser(forceReload?: boolean): Promise<User> {
    const {
      firstName,
      lastName,
      email: contactEmail
    } = await this.keycloakService.loadUserProfile(forceReload);

    const userId = await this.getUserId();

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

  public isApplicant(): boolean {
    return this.hasRole('prime_user');
  }

  public isProvisioner(): boolean {
    return this.hasRole('prime_admin');
  }

  public isAdmin(): boolean {
    return this.hasRole('prime_admin');
  }

  public async decodeToken(): Promise<Keycloak.KeycloakTokenParsed | null> {
    const token = await this.keycloakService.getToken();
    return (token) ? this.jwtHelper.decodeToken(token) : null;
  }

  public login(options?: Keycloak.KeycloakLoginOptions): Promise<void> {
    return this.keycloakService.login(options);
  }

  public isLoggedIn(): Promise<boolean> {
    return this.keycloakService.isLoggedIn();
  }

  public logout(redirectUri?: string): Promise<void> {
    return this.keycloakService.logout(redirectUri);
  }

  public isTokenExpired(): boolean {
    return this.keycloakService.isTokenExpired();
  }

  public clearToken() {
    this.keycloakService.clearToken();
  }
}
