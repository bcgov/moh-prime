import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { HealthAuthority } from '@shared/models/health-authority.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response, and made immutable.
 */
@Injectable({
  providedIn: 'root'
})
export class HealthAuthorityService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _healthAuthority: BehaviorSubject<HealthAuthority | null>;

  constructor() {
    this._healthAuthority = new BehaviorSubject<HealthAuthority | null>(null);
  }

  public set healthAuthority(healthAuthority: HealthAuthority) {
    // Make immutable to prevent changes to the model
    this._healthAuthority.next(healthAuthority);
  }

  public get healthAuthority(): HealthAuthority | null {
    return this._healthAuthority.value ?? null;
  }

  public get healthAuthority$(): Observable<HealthAuthority | null> {
    // Allow subscriptions, but make the subject immutable
    return this._healthAuthority.asObservable();
  }
}
