import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { GisEnrolment } from '../models/gis-enrolment.model';

export interface IEnrolmentService<T> {
  enrolment: T;
  enrolment$: Observable<T>;
}

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
@Injectable({
  providedIn: 'root'
})
export class GisEnrolmentService implements IEnrolmentService<GisEnrolment> {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _enrolment: BehaviorSubject<GisEnrolment>;

  constructor() {
    this._enrolment = new BehaviorSubject<GisEnrolment>(null);
  }

  public set enrolment(enrolment: GisEnrolment) {
    // Store a copy to prevent updates by reference
    this._enrolment.next({ ...enrolment });
  }

  public get enrolment(): GisEnrolment {
    // Allow access to current value, but prevent updates by reference
    const value = this._enrolment.value;
    return (value) ? { ...this._enrolment.value } : null;
  }

  public get enrolment$(): Observable<GisEnrolment> {
    // Allow subscriptions, but make the subject immutable
    return this._enrolment.asObservable();
  }
}
