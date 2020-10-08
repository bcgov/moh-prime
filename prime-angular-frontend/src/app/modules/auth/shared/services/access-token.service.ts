import { Injectable } from '@angular/core';

import { JwtHelperService } from '@auth0/angular-jwt';
import { KeycloakService } from 'keycloak-angular';
import { KeycloakLoginOptions } from 'keycloak-js';

import { BrokerProfile } from '@auth/shared/models/broker-profile.model';
import { AccessTokenParsed } from '@auth/shared/models/access-token-parsed.model';

export interface IAccessTokenService {
  token(): Promise<string>;
  isTokenExpired(): boolean;
  decodeToken(): Promise<AccessTokenParsed | null>;
  clearToken(): void;
  login(options?: KeycloakLoginOptions): Promise<void>;
  isLoggedIn(): Promise<boolean>;
  loadBrokerProfile(forceReload?: boolean): Promise<BrokerProfile>;
  roles(allRoles?: boolean): string[];
  hasRole(role: string): boolean;
  logout(redirectUri: string): Promise<void>;
}

@Injectable({
  providedIn: 'root'
})
export class AccessTokenService implements IAccessTokenService {
  private jwtHelper: JwtHelperService;

  constructor(
    private keycloakService: KeycloakService
  ) {
    this.jwtHelper = new JwtHelperService();
  }

  public async token(): Promise<string> {
    return await this.keycloakService.getToken();
  }

  public isTokenExpired(): boolean {
    return this.keycloakService.isTokenExpired();
  }

  public async decodeToken(): Promise<AccessTokenParsed | null> {
    const token = await this.token();
    return (token) ? this.jwtHelper.decodeToken(token) : null;
  }

  public clearToken(): void {
    this.keycloakService.clearToken();
  }

  public login(options?: KeycloakLoginOptions): Promise<void> {
    return this.keycloakService.login(options);
  }

  public isLoggedIn(): Promise<boolean> {
    return this.keycloakService.isLoggedIn();
  }

  public loadBrokerProfile(forceReload?: boolean): Promise<BrokerProfile> {
    return this.keycloakService.loadUserProfile(forceReload) as Promise<BrokerProfile>;
  }

  public roles(allRoles?: boolean): string[] {
    return this.keycloakService.getUserRoles(allRoles);
  }

  public hasRole(role: string, resource?: string): boolean {
    return this.keycloakService.isUserInRole(role, resource);
  }

  public logout(redirectUri: string): Promise<void> {
    return this.keycloakService.logout(redirectUri);
  }
}
