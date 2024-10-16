import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';

export interface ISatEnrolleeService {
  enrollee: SatEnrollee;
}

@Injectable({
  providedIn: 'root'
})
export class SatEnrolleeService implements ISatEnrolleeService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _enrollee: BehaviorSubject<SatEnrollee>;

  constructor() {
    this._enrollee = new BehaviorSubject<SatEnrollee>(null);
  }

  public set enrollee(enrollee: SatEnrollee) {
    // Store a copy to prevent updates by reference
    this._enrollee.next({ ...enrollee });
  }

  public get enrollee(): SatEnrollee {
    // Allow access to current value, but prevent updates by reference
    const value = this._enrollee.value;
    return (value) ? { ...this._enrollee.value } : null;
  }
}
