import * as faker from 'faker';
import { KeycloakTokenParsed, KeycloakLoginOptions } from 'keycloak-js';

import { Role } from '@auth/shared/enum/role.enum';
import { IAuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';

export class MockAuthService implements IAuthService {

  // tslint:disable-next-line: variable-name
  private _role: Role;
  // tslint:disable-next-line: variable-name
  private _loggedIn: boolean;

  public hasJustLoggedIn: boolean;

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

  public getUser(forceReload?: boolean): Promise<User> {
    return new Promise((resolve, reject) => resolve({
      // TODO what data is required for POS?
      userId: `${faker.random.uuid()}`,
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      dateOfBirth: faker.date.past().toISOString(),
      physicalAddress: {
        countryCode: faker.address.countryCode(),
        provinceCode: faker.address.stateAbbr(),
        street: faker.address.streetAddress(),
        city: faker.address.city(),
        postal: faker.address.zipCode()
      },
      contactEmail: faker.internet.email()
    }));
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
  // TODO what role is accessing the POS?
  public isEnrollee(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      resolve(this._role === Role.ENROLLEE);
    });
  }

  public decodeToken(): Promise<KeycloakTokenParsed> {
    throw new Error('Method not implemented.');
  }

  public login(options?: KeycloakLoginOptions): Promise<void> {
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

  public setHasJustLoggedIn(hasJustLoggedIn: boolean) {
    this.hasJustLoggedIn = hasJustLoggedIn;
  }

  public getHasJustLoggedIn(): boolean {
    return this.hasJustLoggedIn;
  }
}
