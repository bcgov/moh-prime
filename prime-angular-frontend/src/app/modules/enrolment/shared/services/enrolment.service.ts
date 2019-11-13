import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { Config } from '@config/config.model';
import { Enrolment } from '@shared/models/enrolment.model';

export interface IEnrolmentService {
  enrolment$: BehaviorSubject<Enrolment>;
  enrolment: Enrolment;
  status: Config<number>;
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

  public get status(): Config<number> {
    return this._enrolment.value.currentStatus.status;
  }
}
