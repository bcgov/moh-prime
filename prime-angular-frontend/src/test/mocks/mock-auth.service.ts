import * as faker from 'faker';
import { KeycloakTokenParsed, KeycloakLoginOptions } from 'keycloak-js';

import { Role } from '@auth/shared/enum/role.enum';
import { IAuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';
import { Admin } from '@auth/shared/models/admin.model';

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

  public checkAssuranceLevel(assuranceLevel: number): Promise<boolean> {
    throw new Error('Method not implemented.');
  }

  public isEnrollee(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      resolve(this._role === Role.ENROLLEE);
    });
  }

  public isAdmin(): boolean {
    return this._role === Role.ADMIN;
  }

  public isSuperAdmin(): boolean {
    return this._role === Role.SUPER_ADMIN;
  }

  public hasAdminView(): boolean {
    return this._role === Role.READONLY_ADMIN;
  }

  hasEnrollee(): boolean {
    return this._role === Role.ENROLLEE;
  }

  public isRegistrant(): boolean {
    return this._role === Role.FEATURE_SITE_REGISTRATION;
  }

  public isCommunityPharmacist(): boolean {
    return this._role === Role.FEATURE_COMMUNITY_PHARMACIST;
  }

  public hasVCIssuance(): boolean {
    return this._role === Role.FEATURE_VC_ISSUANCE;
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
