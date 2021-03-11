import { KeycloakLoginOptions } from 'keycloak-js';

import { BrokerProfile } from '@auth/shared/models/broker-profile.model';
import { AccessTokenParsed } from '@auth/shared/models/access-token-parsed.model';
import { Role } from '@auth/shared/enum/role.enum';
import { IAccessTokenService } from '@auth/shared/services/access-token.service';

export class MockAccessTokenService implements IAccessTokenService {
  public hasJustLoggedIn: boolean;
  // tslint:disable-next-line: variable-name
  private _role: Role;
  // tslint:disable-next-line: variable-name
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

  public async token(): Promise<string> {
    throw new Error('Method not implemented.');
  }

  public isTokenExpired(): boolean {
    throw new Error('Method not implemented.');
  }

  public async decodeToken(): Promise<AccessTokenParsed | null> {
    return new Promise((resolve, reject) => {
      const token: AccessTokenParsed = <AccessTokenParsed>{};
      resolve(token)
    });
  }

  public clearToken(): void {
    throw new Error('Method not implemented.');
  }

  public login(options?: KeycloakLoginOptions): Promise<void> {
    throw new Error('Method not implemented.');
  }

  public isLoggedIn(): Promise<boolean> {
    return new Promise((resolve, reject) =>
      (this._loggedIn)
        ? resolve(true)
        : reject(false)
    );
  }

  public loadBrokerProfile(forceReload?: boolean): Promise<BrokerProfile> {
    return new Promise((resolve, reject) => {
      const profile: BrokerProfile = {
        attributes: {
          birthdate: [],
          country: [],
          region: [],
          givenNames: [],
          locality: [],
          postalCode: [],
          streetAddress: [],
        }
      };
      return resolve(profile);
    });
  }

  public roles(allRoles?: boolean): string[] {
    throw new Error('Method not implemented.');
  }

  public hasRole(role: string, resource?: string): boolean {
    throw new Error('Method not implemented.');
  }

  public logout(redirectUri: string): Promise<void> {
    throw new Error('Method not implemented.');
  }
}
