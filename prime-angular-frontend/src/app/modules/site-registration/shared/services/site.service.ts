import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { SiteResource } from '@core/resources/site-resource.service';
import { Site } from '@registration/shared/models/site.model';

/**
 * @description
 * Service is considered a source of truth and should be set
 * directly from a HTTP response.
 */
@Injectable({
  providedIn: 'root'
})
export class SiteService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _site: BehaviorSubject<Site>;

  constructor(
    private siteResource: SiteResource
  ) {
    this._site = new BehaviorSubject<Site>(null);
  }

  public set site(site: Site) {
    // Store a copy to prevent updates by reference
    this._site.next({ ...site });
  }

  public get site(): Site {
    // Allow access to current value, but prevent updates by reference
    const value = this._site.value;
    return (value) ? { ...this._site.value } : null;
  }

  public get site$(): Observable<Site> {
    // Allow subscriptions, but make the subject immutable
    return this._site.asObservable();
  }
}
