import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { OrganizationAgreement } from '@shared/models/agreement.model';

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
  // TODO PRIME-1131
  // Temporary hack to show success message until guards can be refactored
  public showSuccess: boolean;

  // tslint:disable-next-line: variable-name
  private _organization: BehaviorSubject<Organization>;

  constructor() {
    this._organization = new BehaviorSubject<Organization>(null);
    this.showSuccess = false;
  }

  public set organization(organization: Organization) {
    // Store a copy to prevent updates by reference
    this._organization.next({ ...organization });
  }

  public get organization(): Organization {
    // Allow access to current value, but prevent updates by reference
    const value = this._organization.value;
    return (value) ? { ...this._organization.value } : null;
  }

  public get organization$(): Observable<Organization> {
    // Allow subscriptions, but make immutable
    return this._organization.asObservable();
  }
}
