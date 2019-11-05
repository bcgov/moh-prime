import { Injectable } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';
import { JwtHelperService } from '@auth0/angular-jwt';

import { LoggerService } from '@core/services/logger.service';
import { Role } from '../enum/role.enum';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends KeycloakService {
  private jwtHelper: JwtHelperService;

  constructor(
    private logger: LoggerService
  ) {
    super();

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
    } = await this.loadUserProfile(forceReload);

    const userId = await this.getUserId();

    return {
      userId,
      firstName,
      lastName,
      contactEmail
    };
  }

  public async isEnrollee(): Promise<boolean> {
    return this.isUserInRole(Role.ENROLLEE) && await this.checkAssuranceLevel(3);
  }

  public isProvisioner(): boolean {
    return this.isUserInRole(Role.PROVISIONER);
  }

  public isAdmin(): boolean {
    return this.isUserInRole(Role.ADMIN);
  }

  public async decodeToken(): Promise<Keycloak.KeycloakTokenParsed | null> {
    const token = await this.getToken();
    return (token) ? this.jwtHelper.decodeToken(token) : null;
  }

  public async checkAssuranceLevel(assuranceLevel: number): Promise<boolean> {
    const token = await this.decodeToken() as any;
    return (token.identity_assurance_level === assuranceLevel);
  }
}
