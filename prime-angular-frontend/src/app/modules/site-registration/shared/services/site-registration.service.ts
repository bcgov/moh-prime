import { Injectable } from '@angular/core';
import { Registrant } from '@shared/models/registrant';
import { BehaviorSubject } from 'rxjs';

export interface ISiteRegistrationService {
  registrant$: BehaviorSubject<Registrant>;
  registrant: Registrant;
}

@Injectable({
  providedIn: 'root'
})
export class SiteRegisrationService implements ISiteRegistrationService {
  // tslint:disable-next-line: variable-name
  private _registrant: BehaviorSubject<Registrant>;

  constructor() {
    this._registrant = new BehaviorSubject<Registrant>(null);
  }

  public get registrant$(): BehaviorSubject<Registrant> {
    return this._registrant;
  }

  public get registrant(): Registrant {
    return this._registrant.value;
  }
}
