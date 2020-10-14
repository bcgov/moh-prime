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
  // Temporary hack to show success message until guards can be refactored
  public showSuccess: boolean;

  // tslint:disable-next-line: variable-name
  private _organization: BehaviorSubject<Organization>;
  // tslint:disable-next-line: variable-name
  private _agreements: BehaviorSubject<OrganizationAgreement[]>;

  constructor() {
    this._organization = new BehaviorSubject<Organization>(null);
    this._agreements = new BehaviorSubject<OrganizationAgreement[]>(null);
    this.showSuccess = false;
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
  // TODO PRIME-1127
  public set agreements(organizationAgreements: OrganizationAgreement[]) {
    // Store a copy to prevent updates by reference
    this._agreements.next([...organizationAgreements]);
  }

  public get agreements(): OrganizationAgreement[] {
    // Allow access to current value, but prevent updates by reference
    return [...this._agreements.value];
  }

  public get agreements$(): Observable<OrganizationAgreement[]> {
    // Allow subscriptions, but make immutable
    return this._agreements.asObservable();
  }

  /**
   * @description
   * Has signed at least one agreement.
   */
  public hasSignedAgreement(): boolean {
    return this.agreements.some((agreement: OrganizationAgreement) => agreement.acceptedDate);
  }
}
