import { Injectable } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';
import { JwtHelperService } from '@auth0/angular-jwt';

import { LoggerService } from '@core/services/logger.service';
import { Role } from '@auth/shared/enum/role.enum';
import { User } from '@auth/shared/models/user.model';
import { Admin } from '../models/admin.model';

export interface IAuthService {
  getUserId(): Promise<string>;
  getUser(forceReload?: boolean): Promise<User>;
  getUserRoles(): string[];
  isUserInRole(role: string): boolean;
  checkAssuranceLevel(assuranceLevel: number): Promise<boolean>;
  isEnrollee(): Promise<boolean>;
  isAdjudicator(): boolean;
  isAdmin(): boolean;
  isSuperAdmin(): boolean;
  decodeToken(): Promise<Keycloak.KeycloakTokenParsed | null>;
  login(options?: Keycloak.KeycloakLoginOptions): Promise<void>;
  isLoggedIn(): Promise<boolean>;
  logout(redirectUri: string): Promise<void>;
  isTokenExpired(): boolean;
  clearToken(): void;
}

export interface KeycloakAttributes {
  attributes: {
    birthdate: string[];
    country: string[];
    region: string[]; // Province
    streetAddress: string[];
    locality: string[]; // City
    postalCode: string[];
  };
}

@Injectable({
  providedIn: 'root'
})
export class AuthService implements IAuthService {
  private jwtHelper: JwtHelperService;

  // Login event state for performing operations
  // required immediately after authentication
  private hasJustLoggedInState: boolean;

  constructor(
    private keycloakService: KeycloakService,
    private logger: LoggerService
  ) {
    this.jwtHelper = new JwtHelperService();
  }

  public set hasJustLoggedIn(hasJustLoggedIn: boolean) {
    this.hasJustLoggedInState = hasJustLoggedIn;
  }

  public get hasJustLoggedIn(): boolean {
    return this.hasJustLoggedInState;
  }

  public async getUserId(): Promise<string> {
    const token = await this.decodeToken();

    this.logger.info('TOKEN', token);

    return token.sub;
  }

  public async getPreferredUsername(): Promise<string> {
    const token = await this.decodeToken() as any;

    return token.preferred_username;
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
        postalCode: [postal] = ''
      }
    } = await this.keycloakService.loadUserProfile(forceReload) as Keycloak.KeycloakProfile & KeycloakAttributes;

    const userId = await this.getUserId();
    const hpdid = await this.getPreferredUsername();

    return {
      userId,
      hpdid,
      firstName,
      lastName,
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
    } = await this.keycloakService.loadUserProfile(forceReload) as Keycloak.KeycloakProfile;

    const userId = await this.getUserId();
    const idir = await this.getPreferredUsername();

    return {
      userId,
      firstName,
      lastName,
      email,
      idir
    };
  }

  public async checkAssuranceLevel(assuranceLevel: number): Promise<boolean> {
    const token = await this.decodeToken() as any;
    return (token.identity_assurance_level === assuranceLevel);
  }

  public getUserRoles(allRoles?: boolean): string[] {
    return this.keycloakService.getUserRoles(allRoles);
  }

  public isUserInRole(role: string): boolean {
    return this.keycloakService.isUserInRole(role);
  }

  public async isEnrollee(): Promise<boolean> {
    return this.isUserInRole(Role.ENROLLEE) && await this.checkAssuranceLevel(3);
  }

  public isAdjudicator(): boolean {
    return this.isUserInRole(Role.ADJUDICATOR);
  }

  public isAdmin(): boolean {
    return this.isUserInRole(Role.ADMIN);
  }

  public isSuperAdmin(): boolean {
    return this.isUserInRole(Role.SUPER_ADMIN);
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

  public logout(redirectUri: string = '/'): Promise<void> {
    return this.keycloakService.logout(redirectUri);
  }

  public isTokenExpired(): boolean {
    return this.keycloakService.isTokenExpired();
  }

  public clearToken() {
    this.keycloakService.clearToken();
  }
}
