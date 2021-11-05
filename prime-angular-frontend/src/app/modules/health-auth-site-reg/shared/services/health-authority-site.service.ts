import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response, and made immutable.
 */
@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _site: BehaviorSubject<HealthAuthoritySite | null>;

  constructor() {
    this._site = new BehaviorSubject<HealthAuthoritySite | null>(null);
  }

  public set site(healthAuthoritySite: HealthAuthoritySite) {
    this._site.next(healthAuthoritySite);
  }

  public get site(): HealthAuthoritySite | null {
    return this._site.value ?? null;
  }

  public get site$(): Observable<HealthAuthoritySite | null> {
    // Allow subscriptions, but make the subject immutable
    return this._site.asObservable();
  }
}
