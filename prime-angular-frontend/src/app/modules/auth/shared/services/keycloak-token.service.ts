import { Injectable } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';
import { JwtHelperService } from '@auth0/angular-jwt';

import { LoggerService } from '@core/services/logger.service';
import { Token } from '@auth/shared/services/token.interface';
import { User } from '@auth/shared/models/user.model';
import { Admin } from '@auth/shared/models/admin.model';

export interface KeycloakAttributes {
  attributes: {
    birthdate: string[];
    country: string[];
    region: string[]; // Province
    streetAddress: string[];
    locality: string[]; // City
    postalCode: string[];
    givenNames: string[];
  };
}

@Injectable({
  providedIn: 'root'
})
export class KeycloakTokenService implements Token {
  private jwtHelper: JwtHelperService;

  constructor(
    private keycloakService: KeycloakService,
    private logger: LoggerService
  ) {
    this.jwtHelper = new JwtHelperService();
  }

  public async token(): Promise<string> {
    return await this.keycloakService.getToken();
  }

  public async decodeToken(): Promise<Keycloak.KeycloakTokenParsed | null> {
    const token = await this.token();
    return (token) ? this.jwtHelper.decodeToken(token) : null;
  }

  public clearToken(): void {
    this.keycloakService.clearToken();
  }

  public isTokenExpired(): boolean {
    return this.keycloakService.isTokenExpired();
  }

  public login(options?: Keycloak.KeycloakLoginOptions): Promise<void> {
    return this.keycloakService.login(options);
  }

  public logout(redirectUri: string): Promise<void> {
    return this.keycloakService.logout(redirectUri);
  }

  public isLoggedIn(): Promise<boolean> {
    return this.keycloakService.isLoggedIn();
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
      email: contactEmail = '',
      attributes: {
        birthdate: [dateOfBirth] = '',
        country: [countryCode] = '',
        region: [provinceCode] = '',
        streetAddress: [street] = '',
        locality: [city] = '',
        postalCode: [postal] = '',
        givenNames: [givenNames] = ''
      }
    } = await this.keycloakService.loadUserProfile(forceReload) as Keycloak.KeycloakProfile & KeycloakAttributes;

    const userId = await this.getUserId();
    const hpdid = await this._getPreferredUsername();

    return {
      userId,
      hpdid,
      firstName,
      lastName,
      givenNames,
      dateOfBirth,
      physicalAddress: {
        countryCode,
        provinceCode,
        street,
        city,
        postal
      },
      contactEmail
    };
  }

  public async getAdmin(forceReload?: boolean): Promise<Admin> {
    const {
      firstName,
      lastName,
      email
    } = await this.keycloakService.loadUserProfile(forceReload);

    const userId = await this.getUserId();
    const idir = await this._getPreferredUsername();

    return {
      userId,
      firstName,
      lastName,
      email,
      idir
    };
  }

  public getUserRoles(allRoles?: boolean): string[] {
    return this.keycloakService.getUserRoles(allRoles);
  }

  public isUserInRole(role: string): boolean {
    if (this.getUserRoles().includes(role)) {
      return true;
    }

    return this.keycloakService.isUserInRole(role);
  }

  private async _getPreferredUsername(): Promise<string> {
    const token = await this.decodeToken() as any;

    return token.preferred_username;
  }
}
