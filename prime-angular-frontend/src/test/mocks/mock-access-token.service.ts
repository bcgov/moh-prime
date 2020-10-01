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
    throw new Error('Method not implemented.');
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
    throw new Error('Method not implemented.');
  }

  public logout(redirectUri: string): Promise<void> {
    throw new Error('Method not implemented.');
  }
}
