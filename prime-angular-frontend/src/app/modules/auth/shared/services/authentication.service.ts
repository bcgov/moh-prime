import { Injectable } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';

import { Role } from '@auth/shared/enum/role.enum';
import { User } from '../models/user.model';
import { Admin } from '../models/admin.model';
import { KeycloakTokenService } from './keycloak-token.service';

export interface IAuthenticationService {
  checkAssuranceLevel(assuranceLevel: number): Promise<boolean>;
  isEnrollee(): Promise<boolean>;
  isAdmin(): boolean;
  isSuperAdmin(): boolean;
  hasAdminView(): boolean;
  hasEnrollee(): boolean;
  isLoggedIn(): Promise<boolean>;
  isRegistrant(): boolean;

  logout(redirectUri?: string): Promise<void>;
  login(options?: any): Promise<void>;
  getUser(forceReload?: boolean): Promise<User>;
  getAdmin(forceReload?: boolean): Promise<Admin>;
}

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements IAuthenticationService {
  // Login event state for performing operations
  // required immediately after authentication
  private hasJustLoggedInState: boolean;

  constructor(
    private keycloakService: KeycloakService,
    private keycloakTokenService: KeycloakTokenService
  ) { }

  public set hasJustLoggedIn(hasJustLoggedIn: boolean) {
    this.hasJustLoggedInState = hasJustLoggedIn;
  }

  public get hasJustLoggedIn(): boolean {
    return this.hasJustLoggedInState;
  }

  public isLoggedIn(): Promise<boolean> {
    return this.keycloakTokenService.isLoggedIn();
  }

  public login(options?: any): Promise<void> {
    return this.keycloakTokenService.login(options);
  }

  public logout(redirectUri: string = '/'): Promise<void> {
    return this.keycloakTokenService.logout(redirectUri);
  }

  // TODO only have a singular method that returns BcscUser or IdirUser
  // TODO use switch case to determine the authenticated user type
  public async getUser(forceReload?: boolean): Promise<User> {
    return this.keycloakTokenService.getUser(forceReload);
  }

  public async getAdmin(forceReload?: boolean): Promise<Admin> {
    return this.keycloakTokenService.getAdmin(forceReload);
  }

  public async checkAssuranceLevel(assuranceLevel: number): Promise<boolean> {
    const token = await this.keycloakTokenService.decodeToken() as any;
    return (token.identity_assurance_level === assuranceLevel);
  }

  // TODO move these into an authorization service
  public async isEnrollee(): Promise<boolean> {
    return this.keycloakTokenService.isUserInRole(Role.ENROLLEE) && await this.checkAssuranceLevel(3);
  }

  public hasEnrollee(): boolean {
    return this.keycloakTokenService.isUserInRole(Role.ENROLLEE);
  }

  public isAdmin(): boolean {
    return this.keycloakTokenService.isUserInRole(Role.ADMIN);
  }

  public isSuperAdmin(): boolean {
    return this.keycloakTokenService.isUserInRole(Role.SUPER_ADMIN);
  }

  public hasAdminView(): boolean {
    return this.keycloakTokenService.isUserInRole(Role.READONLY_ADMIN);
  }

  public isRegistrant(): boolean {
    return this.keycloakTokenService.isUserInRole(Role.FEATURE_SITE_REGISTRATION);
  }

  public isCommunityPharmacist(): boolean {
    return this.keycloakTokenService.isUserInRole(Role.FEATURE_COMMUNITY_PHARMACIST);
  }
}
