import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { Site } from '@registration/shared/models/site.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';

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
    // Allow subscriptions, but make immutable
    return this._site.asObservable();
  }

  public businessLicenceUpdates(
    siteId: number,
    oldBusinessLicence: BusinessLicence,
    newBusinessLicence: BusinessLicence & { businessLicenceGuid }
  ): Observable<BusinessLicence | BusinessLicenceDocument | void>[] {
    const documentGuid = newBusinessLicence.businessLicenceGuid ?? null;

    if (!oldBusinessLicence?.id) {
      // Create a business licence when none existed
      return [this.siteResource.createBusinessLicence(siteId, newBusinessLicence, documentGuid)];
    }

    newBusinessLicence.id = oldBusinessLicence.id;

    if (oldBusinessLicence.deferredLicenceReason !== newBusinessLicence.deferredLicenceReason) {
      // Remove an existing business licence document before updating
      // with a reason for deferment
      return [
        ...ArrayUtils.insertResultIf(
          oldBusinessLicence?.businessLicenceDocument,
          () => [this.siteResource.removeBusinessLicenceDocument(siteId, oldBusinessLicence.id)]
        ),
        this.siteResource.updateBusinessLicence(siteId, newBusinessLicence)
      ];
    }

    // Create a business licence document each time a file is uploaded, and/or
    // update an existing business licence
    return [
      ...ArrayUtils.insertResultIf(
        documentGuid,
        () => [this.siteResource.createBusinessLicenceDocument(siteId, oldBusinessLicence.id, documentGuid)]
      ),
      this.siteResource.updateBusinessLicence(siteId, newBusinessLicence)
    ];
  }
}
