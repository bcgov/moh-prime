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

  /**
   * @description
   * Perform the updates required for a site business licence.
   */
  public businessLicenceUpdates(
    siteId: number,
    currentBusinessLicence: BusinessLicence,
    updatedBusinessLicence: BusinessLicence,
    documentGuid: string = null
  ): Observable<BusinessLicence | BusinessLicenceDocument | void>[] {
    if (!currentBusinessLicence?.id) {
      // Create a business licence when none existed
      return [this.siteResource.createBusinessLicence(siteId, updatedBusinessLicence, documentGuid)];
    }

    updatedBusinessLicence.id = currentBusinessLicence.id;

    if (currentBusinessLicence.deferredLicenceReason !== updatedBusinessLicence.deferredLicenceReason) {
      // Remove an existing business licence document before updating
      // with a reason for deferment
      return [
        ...ArrayUtils.insertResultIf(
          currentBusinessLicence?.businessLicenceDocument,
          () => [this.siteResource.removeBusinessLicenceDocument(siteId, currentBusinessLicence.id)]
        ),
        this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence)
      ];
    }

    // Create a business licence document each time a file is uploaded, and
    // update the existing business licence
    return [
      ...ArrayUtils.insertResultIf(
        documentGuid,
        () => [
          this.siteResource.removeBusinessLicenceDocument(siteId, currentBusinessLicence.id),
          this.siteResource.createBusinessLicenceDocument(siteId, currentBusinessLicence.id, documentGuid)
        ]
      ),
      this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence)
    ];
  }
}
