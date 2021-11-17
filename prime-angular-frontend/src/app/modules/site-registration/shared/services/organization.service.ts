import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { Organization } from '@registration/shared/models/organization.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
export interface IOrganizationService {
  organization$: Observable<Organization | null>;
  organization: Organization | null;
}

@Injectable({
  providedIn: 'root'
})
export class OrganizationService implements IOrganizationService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _organization: BehaviorSubject<Organization>;

  constructor() {
    this._organization = new BehaviorSubject<Organization>(null);
  }

  public set organization(organization: Organization) {
    // Store a copy to prevent updates by reference
    this._organization.next((organization) ? { ...organization } : null);
  }

  public get organization(): Organization | null {
    // Allow access to current value, but prevent updates by reference
    const value = this._organization.value;
    return (value) ? { ...this._organization.value } : null;
  }

  public get organization$(): Observable<Organization | null> {
    // Allow subscriptions, but make the subject immutable
    return this._organization.asObservable();
  }
}
