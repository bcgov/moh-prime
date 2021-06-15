import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { HealthAuthSite } from '@health-auth/shared/models/health-auth-site.model';

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
  private _site: BehaviorSubject<HealthAuthSite>;

  constructor() {
    this._site = new BehaviorSubject<HealthAuthSite>(null);
  }

  public set site(site: HealthAuthSite) {
    // Store a copy to prevent updates by reference
    this._site.next({ ...site });
  }

  public get site(): HealthAuthSite {
    // Allow access to current value, but prevent updates by reference
    const value = this._site.value;
    return (value) ? { ...this._site.value } : null;
  }

  public get site$(): Observable<HealthAuthSite> {
    // Allow subscriptions, but make immutable
    return this._site.asObservable();
  }
}
