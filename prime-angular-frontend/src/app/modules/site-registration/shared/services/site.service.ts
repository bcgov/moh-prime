import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { Site } from '@registration/shared/models/site.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
export interface ISiteService {
  site$: BehaviorSubject<Site>;
  site: Site;
}

@Injectable({
  providedIn: 'root'
})
export class SiteService {
  // tslint:disable-next-line: variable-name
  private _site: BehaviorSubject<Site>;

  constructor() {
    this._site = new BehaviorSubject<Site>(null);
  }

  public set site(site: Site) {
    // Store a copy to prevent updates by reference
    this._site.next({ ...site });
  }

  public get site(): Site {
    // Allow access to current value, but prevent updates by reference
    return { ...this._site.value };
  }

  public get site$(): Observable<Site> {
    // Allow subscriptions, but make immutable
    return this._site.asObservable();
  }
}
