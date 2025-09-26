import { Injectable } from '@angular/core';

import { from, Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { KeycloakLoginOptions } from 'keycloak-js';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { Address } from '@lib/models/address.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { Admin } from '@auth/shared/models/admin.model';
import { AccessTokenParsed } from '@auth/shared/models/access-token-parsed.model';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AccessTokenService } from '@auth/shared/services/access-token.service';

export interface IAuthService {
  login(options?: KeycloakLoginOptions): Promise<void>;
  isLoggedIn(): Promise<boolean>;
  identityProvider(): Promise<IdentityProviderEnum>;
  identityProvider$(): Observable<IdentityProviderEnum>;
  logout(redirectUri: string): Promise<void>;
  getUser(forceReload?: boolean): Promise<BcscUser>;
  getUser$(forceReload?: boolean): Observable<BcscUser>;
  getAdmin(forceReload?: boolean): Promise<Admin>;
  getAdmin$(forceReload?: boolean): Observable<Admin>;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService implements IAuthService {
  // Login event state for performing operations
  // required immediately after authentication
  private hasJustLoggedInState: boolean;
  private haPasscode: string;

  constructor(
    private accessTokenService: AccessTokenService,
    private logger: ConsoleLoggerService
  ) { }

  public set hasJustLoggedIn(hasJustLoggedIn: boolean) {
    this.hasJustLoggedInState = hasJustLoggedIn;
  }

  public get hasJustLoggedIn(): boolean {
    return this.hasJustLoggedInState;
  }

  public set passcode(passcode: string) {
    this.haPasscode = passcode;
  }

  public get passcode(): string {
    return this.haPasscode;
  }

  public login(options?: any): Promise<void> {
    return this.accessTokenService.login(options);
  }

  public isLoggedIn(): Promise<boolean> {
    return this.accessTokenService.isLoggedIn();
  }

  /**
   * @description
   * If BCSC was used to authenticate, we want IdentityProvider to equal "bcsc"
   * regardless of the IDP alias in a particular instance of KeyCloak
   */
  public async identityProvider(): Promise<IdentityProviderEnum> {
    return await this.accessTokenService.decodeToken()
      .then((token: AccessTokenParsed) =>
        (IdentityProviderEnum.BCSC_MOH === token.identity_provider ? IdentityProviderEnum.BCSC : token.identity_provider));
  }

  public identityProvider$(): Observable<IdentityProviderEnum> {
    return from(this.identityProvider());
  }

  public logout(redirectUri: string = '/'): Promise<void> {
    return this.accessTokenService.logout(redirectUri);
  }

  /**
   * @description
   * Get the authenticated user.
   *
   * NOTE: Careful using this in Angular lifecycle hooks. It is preferrable to
   * use the Observable based method getUser$().
   */
  // TODO should be based on provider now
  // TODO use this as a base method for all other types of users
  // TODO multiple return types through switch-case, and new up objects for narrowing
  public async getUser(forceReload?: boolean): Promise<BcscUser> {
    const token = await this.accessTokenService.decodeToken();

    const firstName = token?.given_name;
    const lastName = token?.family_name;
    const email = '';
    const dateOfBirth = token?.birthdate;
    const countryCode = token?.address?.country;
    const provinceCode = token?.address?.region;
    const street = token?.address?.street_address;
    const city = token?.address?.locality;
    const postal = token?.address?.postal_code;
    const givenNames = token?.given_names;

    const userId = token?.sub;
    const username = token?.preferred_username;  // Expecting e.g. gtcochh2vajdtodkby27kspv554dn4is@bcsc

    const claims = await this.getTokenAttribsByKey('bcsc_guid');  // e.g. from MoH KeyCloak   "bcsc_guid": "GTCOCHH2VAJDTODKBY27KSPV554DN4IS"

    const mapping = {
      bcsc_guid: 'hpdid'
    };
    ObjectUtils.keyMapping(claims, mapping);
    claims['hpdid'] = claims['hpdid']?.toLowerCase();

    // BCSC does not guarantee an address
    const address = { countryCode, provinceCode, street, city, postal } as Address;
    const verifiedAddress = (Address.isNotEmpty(address))
      ? address
      : null; // Explicit indicator that address does not exist

    return {
      userId,
      username,
      firstName,
      lastName,
      givenNames,
      dateOfBirth,
      verifiedAddress,
      email,
      ...claims
    } as BcscUser;
  }

  public getUser$(forceReload?: boolean): Observable<BcscUser> {
    return from(this.getUser(forceReload)).pipe(take(1));
  }

  /**
   * @description
   * Get the authenticated user.
   *
   * NOTE: Careful using this in Angular lifecycle hooks. It is preferrable to
   * use the Observable based method getAdmin$().
   */
  // TODO drop after getUser provides object instance of auth user
  public async getAdmin(forceReload?: boolean): Promise<Admin> {
    const token = await this.accessTokenService.decodeToken();

    const firstName = token?.given_name;
    const lastName = token?.family_name;
    const email = token?.email;
    const userId = token?.sub;
    const username = token?.preferred_username;

    const claims = await this.getTokenAttribsByKey('preferred_username');

    const mapping = {
      preferred_username: 'idir'
    };
    ObjectUtils.keyMapping(claims, mapping);

    return {
      userId,
      firstName,
      lastName,
      email,
      username,
      ...claims as { idir: string }
    } as Admin;
  }

  public getAdmin$(forceReload?: boolean): Observable<Admin> {
    return from(this.getAdmin(forceReload)).pipe(take(1));
  }

  private async getTokenAttribsByKey(keys: string | string[]): Promise<{ [key: string]: any }> {
    const token = await this.accessTokenService.decodeToken();

    return (Array.isArray(keys))
      ? keys.reduce((attribs: { [key: string]: any }, key: string) => ObjectUtils.mergeInto(key, token, attribs), {})
      : ObjectUtils.mergeInto(keys, token);
  }
}
