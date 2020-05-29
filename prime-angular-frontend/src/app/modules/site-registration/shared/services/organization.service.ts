import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { Organization } from '@registration/shared/models/organization.model';

export interface ISiteRegistrationService {
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

  public get organization$(): BehaviorSubject<Organization> {
    return this._organization;
  }

  public get organization(): Organization {
    return this._organization.value;
  }
}
