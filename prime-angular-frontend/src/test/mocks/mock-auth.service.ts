import { Role } from '@auth/shared/enum/role.enum';
import { IAuthService } from '@auth/shared/services/auth.service';

export class MockAuthService implements IAuthService {
  private _role: Role;
  private _loggedIn: boolean;

  constructor(
  ) {
    this._loggedIn = false;
  }

  public set role(role: Role) {
    this._role = role;
  }

  public set loggedIn(loggedIn: boolean) {
    this._loggedIn = loggedIn;
  }

  public getUserId(): Promise<string> {
    throw new Error('Method not implemented.');
  }

  public getUser(forceReload?: boolean): Promise<import('../../app/modules/auth/shared/models/user.model').User> {
    throw new Error('Method not implemented.');
  }

  public getUserRoles(): string[] {
    return [this._role];
  }

  public isUserInRole(role: string): boolean {
    return this.getUserRoles().includes(role);
  }

  public checkAssuranceLevel(assuranceLevel: number): Promise<boolean> {
    throw new Error('Method not implemented.');
  }

  public isEnrollee(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      resolve(this._role === Role.ENROLLEE);
    });
  }

  public isProvisioner(): boolean {
    return this._role === Role.PROVISIONER;
  }

  public isAdmin(): boolean {
    return this._role === Role.ADMIN;
  }

  public decodeToken(): Promise<import('keycloak-js').KeycloakTokenParsed> {
    throw new Error('Method not implemented.');
  }

  public login(options?: import('keycloak-js').KeycloakLoginOptions): Promise<void> {
    throw new Error('Method not implemented.');
  }

  public isLoggedIn(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this._loggedIn
      ? resolve(true)
      : reject(false);
    });
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
