import { KeycloakOptions, KeycloakService } from 'keycloak-angular';

import { environment } from '@env/environment';
import { AuthService, IAuthService } from '@auth/shared/services/auth.service';

export class MockAuthService implements IAuthService {
  constructor(
    private keycloakService: KeycloakService,
    // Must use the AuthService within tests
    // private authService: AuthService
  ) {
    this.keycloakService.init(environment.keycloakConfig as KeycloakOptions);
  }

  public getUserId(): Promise<string> {
    throw new Error('Method not implemented.');
  }

  public getUser(forceReload?: boolean): Promise<import('../../app/modules/auth/shared/models/user.model').User> {
    throw new Error('Method not implemented.');
  }

  public getUserRoles(): string[] {
    throw new Error('Method not implemented.');
  }

  public isUserInRole(role: string): boolean {
    throw new Error('Method not implemented.');
  }

  public checkAssuranceLevel(assuranceLevel: number): Promise<boolean> {
    throw new Error('Method not implemented.');
  }

  public isEnrollee(): Promise<boolean> {
    throw new Error('Method not implemented.');
  }

  public isProvisioner(): boolean {
    // return true;
    // return this.authService.isProvisioner();
  }

  public isAdmin(): boolean {
    throw new Error('Method not implemented.');
  }

  public decodeToken(): Promise<import('keycloak-js').KeycloakTokenParsed> {
    throw new Error('Method not implemented.');
  }

  public login(options?: import('keycloak-js').KeycloakLoginOptions): Promise<void> {
    throw new Error('Method not implemented.');
  }

  public isLoggedIn(): Promise<boolean> {
    throw new Error('Method not implemented.');
  }

  public logout(redirectUri: string): Promise<void> {
    throw new Error('Method not implemented.');
  }

  public isTokenExpired(): boolean {
    throw new Error('Method not implemented.');
  }

  public clearToken(): void {
    throw new Error('Method not implemented.');
  }
}
