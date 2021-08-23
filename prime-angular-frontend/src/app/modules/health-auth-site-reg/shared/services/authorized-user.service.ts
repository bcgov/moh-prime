import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { Organization } from '@registration/shared/models/organization.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
export interface IAuthorizedUserService {
  authorizedUser$: Observable<AuthorizedUser | null>;
  authorizedUser: AuthorizedUser | null;
}

@Injectable({
  providedIn: 'root'
})
export class AuthorizedUserService implements IAuthorizedUserService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _authorizedUser: BehaviorSubject<AuthorizedUser | null>;

  constructor() {
    this._authorizedUser = new BehaviorSubject<AuthorizedUser | null>(null);
  }

  public set authorizedUser(authorizedUser: AuthorizedUser | null) {
    // Store a copy to prevent updates by reference
    this._authorizedUser.next((authorizedUser) ? { ...authorizedUser } : null);
  }

  public get authorizedUser(): AuthorizedUser | null {
    // Allow access to current value, but prevent updates by reference
    const value = this._authorizedUser.value;
    return (value) ? { ...this._authorizedUser.value } : null;
  }

  public get authorizedUser$(): Observable<AuthorizedUser | null> {
    // Allow subscriptions, but make immutable
    return this._authorizedUser.asObservable();
  }
}
