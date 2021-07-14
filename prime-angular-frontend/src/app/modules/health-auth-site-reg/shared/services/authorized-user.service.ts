import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { AuthorizedUser } from '@shared/models/authorized-user.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
@Injectable({
  providedIn: 'root'
})
export class AuthorizedUserService {
  // tslint:disable-next-line: variable-name
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
}
