import { Injectable } from '@angular/core';
import { Registrant } from '@shared/models/registrant';
import { BehaviorSubject } from 'rxjs';

export interface IRegistrantService {
  registrant$: BehaviorSubject<Registrant>;
  registrant: Registrant;
}

@Injectable({
  providedIn: 'root'
})
export class RegistrantService implements IRegistrantService {
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
