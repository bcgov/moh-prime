import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { Party } from '@lib/models/party.model';

@Injectable({
  providedIn: 'root'
})
export class SigningAuthorityService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _signingAuthority: BehaviorSubject<Party>;

  constructor() {
    this._signingAuthority = new BehaviorSubject<Party>(null);
  }

  public set signingAuthority(signingAuthority: Party) {
    // Store a copy to prevent updates by reference
    this._signingAuthority.next({ ...signingAuthority });
  }

  public get signingAuthority(): Party {
    // Allow access to current value, but prevent updates by reference
    const value = this._signingAuthority.value;
    return (value) ? { ...this._signingAuthority.value } : null;
  }

  public get signingAuthority$(): Observable<Party> {
    // Allow subscriptions, but make the subject immutable
    return this._signingAuthority.asObservable();
  }
}
