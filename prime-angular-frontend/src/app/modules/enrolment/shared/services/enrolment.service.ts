import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';

export interface IEnrolmentService {
  enrolment$: BehaviorSubject<Enrolment>;
  enrolment: Enrolment;
  isInitialEnrolment: boolean;
  isProfileComplete: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class EnrolmentService implements IEnrolmentService {
  // tslint:disable-next-line: variable-name
  private _enrolment: BehaviorSubject<Enrolment>;

  constructor() {
    this._enrolment = new BehaviorSubject<Enrolment>(null);
  }

  public get enrolment$(): BehaviorSubject<Enrolment> {
    return this._enrolment;
  }

  public get enrolment(): Enrolment {
    return this._enrolment.value;
  }

  public get isInitialEnrolment(): boolean {
    return (this.enrolment)
      ? !!this.enrolment.expiryDate
      : false;
  }

  public get isProfileComplete(): boolean {
    return (this.enrolment)
      ? this.enrolment.profileCompleted
      : false;
  }
}
