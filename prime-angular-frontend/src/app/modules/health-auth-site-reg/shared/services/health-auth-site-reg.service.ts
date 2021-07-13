import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
@Injectable({
  providedIn: 'root'
})
export class HealthAuthSiteRegService {
  // tslint:disable-next-line: variable-name
  private _site: BehaviorSubject<HealthAuthoritySite>;

  constructor() {
    this._site = new BehaviorSubject<HealthAuthoritySite>(null);
  }

  public set site(site: HealthAuthoritySite) {
    // Store a copy to prevent updates by reference
    this._site.next({ ...site });
  }

  public get site(): HealthAuthoritySite {
    // Allow access to current value, but prevent updates by reference
    const value = this._site.value;
    return (value) ? { ...this._site.value } : null;
  }

  public get site$(): Observable<HealthAuthoritySite> {
    // Allow subscriptions, but make immutable
    return this._site.asObservable();
  }
}
