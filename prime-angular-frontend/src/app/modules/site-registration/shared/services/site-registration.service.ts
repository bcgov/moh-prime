import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { Site } from '../models/site.model';

export interface ISiteRegistrationService {
  site$: BehaviorSubject<Site>;
  site: Site;
}

@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationService implements ISiteRegistrationService {
  // tslint:disable-next-line: variable-name
  private _site: BehaviorSubject<Site>;

  constructor() {
    this._site = new BehaviorSubject<Site>(null);
  }

  public get site$(): BehaviorSubject<Site> {
    return this._site;
  }

  public get site(): Site {
    return this._site.value;
  }
}
