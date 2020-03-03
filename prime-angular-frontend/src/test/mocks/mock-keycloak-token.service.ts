import { Token } from '@auth/shared/services/token.interface';
import { User } from '@auth/shared/models/user.model';
import { Admin } from '@auth/shared/models/admin.model';
import { Role } from '@auth/shared/enum/role.enum';
import * as faker from 'faker';
import { KeycloakTokenParsed, KeycloakLoginOptions } from 'keycloak-js';

export class MockKeycloakTokenService implements Token {
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
      userId: `${faker.random.uuid()}`,
      hpdid: `${faker.random.uuid()}`,
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

  public getAdmin(forceReload?: boolean): Promise<Admin> {
    return new Promise((resolve, reject) => resolve({
      userId: `${faker.random.uuid()}`,
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      email: faker.internet.email(),
      idir: `${faker.random.uuid()}`,
    }));
  }

  public getUserRoles(): string[] {
    return [this._role];
  }

  public isUserInRole(role: string): boolean {
    return this.getUserRoles().includes(role);
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

}
