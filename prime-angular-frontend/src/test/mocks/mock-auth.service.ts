import * as faker from 'faker';
import { KeycloakLoginOptions } from 'keycloak-js';

import { Role } from '@auth/shared/enum/role.enum';
import { IAuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { Admin } from '@auth/shared/models/admin.model';
import { Observable, from } from 'rxjs';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';

export class MockAuthService implements IAuthService {
  // eslint-disable-next-line @typescript-eslint/naming-convention,no-underscore-dangle,id-blacklist,id-match
  private _role: Role;
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
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

  public async identityProvider(): Promise<IdentityProviderEnum> {
    return await Promise.resolve(IdentityProviderEnum.BCSC);
  }

  public identityProvider$(): Observable<IdentityProviderEnum> {
    return from(this.identityProvider());
  }

  public logout(redirectUri: string): Promise<void> {
    throw new Error('Method not implemented.');
  }

  public getUser(forceReload?: boolean): Promise<BcscUser> {
    return new Promise((resolve, reject) => resolve({
      userId: `${faker.random.uuid()}`,
      hpdid: `${faker.random.uuid()}`,
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      givenNames: faker.name.firstName(),
      dateOfBirth: faker.date.past().toISOString(),
      verifiedAddress: {
        countryCode: faker.address.countryCode(),
        provinceCode: faker.address.stateAbbr(),
        street: faker.address.streetAddress(),
        city: faker.address.city(),
        postal: faker.address.zipCode()
      },
      email: faker.internet.email()
    }));
  }

  public getUser$(forceReload?: boolean): Observable<BcscUser> {
    return from(this.getUser());
  }

  public getAdmin(forceReload?: boolean): Promise<Admin> {
    return new Promise((resolve, reject) => resolve({
      // TODO drop use of idir through mapping
      // username: `${faker.internet.userName()}`,
      userId: `${faker.random.uuid()}`,
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      email: faker.internet.email(),
      idir: `${faker.random.uuid()}`
    }));
  }

  public getAdmin$(forceReload?: boolean): Observable<Admin> {
    return from(this.getAdmin());
  }
}
