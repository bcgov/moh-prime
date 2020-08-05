import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { Organization } from '@registration/shared/models/organization.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
export interface IOrganizationService {
  organization$: BehaviorSubject<Organization>;
  organization: Organization;
}

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {
  // tslint:disable-next-line: variable-name
  private _organization: BehaviorSubject<Organization>;

  constructor() {
    this._organization = new BehaviorSubject<Organization>(null);
  }

  public set organization(organization: Organization) {
    // Store a copy to prevent updates by reference
    this._organization.next({ ...organization });
  }

  public get organization(): Organization {
    // Allow access to current value, but prevent updates by reference
    return { ...this._organization.value };
  }

  public get organization$(): Observable<Organization> {
    // Allow subscriptions, but make immutable
    return this._organization.asObservable();
  }
}
