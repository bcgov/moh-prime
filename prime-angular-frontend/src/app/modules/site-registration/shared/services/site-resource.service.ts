import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import * as moment from 'moment';

import { compare, Operation } from 'fast-json-patch';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { NoContent } from '@core/resources/abstract-resource';
import { BusinessDay } from '@lib/modules/business-hours/models/business-day.model';

import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { BusinessLicence } from '../models/business-licence.model';
import { Location } from '@registration/shared/models/location.model';


// TODO use ApiResourceUtils to build URLs
// TODO split out log messages for reuse into ErrorHandler
@Injectable({
  providedIn: 'root'
})
export class SiteResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public getSites(organizationId: number): Observable<Site[]> {
    return this.apiResource.get<Site[]>(`organizations/${organizationId}/sites`)
      .pipe(
        map((response: ApiHttpResponse<Site[]>) => response.result),
        // TODO split out into proper adapter
        map((sites: Site[]) => {
          sites.map((site: Site) => {
            site.location.businessHours = site.location.businessHours.map((businessDay: BusinessDay) => {
              businessDay.startTime = `${moment.duration(businessDay.startTime).asHours()}`;
              businessDay.endTime = `${moment.duration(businessDay.endTime).asHours()}`;

              if (businessDay.endTime === '24') {
                businessDay.startTime = null;
                businessDay.endTime = null;
              }
              return businessDay;
            });
          });
          return sites;
        }),
        tap((sites: Site[]) => this.logger.info('SITES', sites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Sites could not be retrieved');
          this.logger.error('[SiteRegistration] SiteResource::getSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteById(siteId: number): Observable<Site> {
    return this.apiResource.get<Site>(`sites/${siteId}`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        // TODO split out into proper adapter
        map((site: Site) => {
          site.location.businessHours = site.location.businessHours.map((businessDay: BusinessDay) => {
            businessDay.startTime = `${moment.duration(businessDay.startTime).asHours()}`;
            businessDay.endTime = `${moment.duration(businessDay.endTime).asHours()}`;

            if (businessDay.endTime === '24') {
              businessDay.startTime = null;
              businessDay.endTime = null;
            }
            return businessDay;
          });
          return site;
        }),
        tap((site: Site) => this.logger.info('SITE', site)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be retrieved');
          this.logger.error('[SiteRegistration] SiteResource::getSiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createSite(organizationId: number): Observable<Site> {
    return this.apiResource.post<Site>(`organizations/${organizationId}/sites`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((newSite: Site) => {
          this.toastService.openSuccessToast('Site has been created');
          this.logger.info('NEW_SITE', newSite);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be created');
          this.logger.error('[SiteRegistration] SiteResource::createSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateSite(site: Site, isCompleted?: boolean): NoContent {
    // TODO separate this out into a proper adapter
    if (site.location.businessHours?.length) {
      site.location.businessHours = site.location.businessHours
        .map((businessDay: BusinessDay) => {
          if (businessDay.startTime === null && businessDay.endTime === null) {
            businessDay.startTime = '0';
            businessDay.endTime = '1.00';
          }
          businessDay.startTime = `${businessDay.startTime}:00:00`;
          businessDay.endTime = `${businessDay.endTime}:00:00`;
          return businessDay;
        });
    } else {
      site.location.businessHours = null;
    }

    const params = this.apiResourceUtilsService.makeHttpParams({ isCompleted });
    return this.apiResource.put<NoContent>(`sites/${site.id}`, site, params)
      // TODO remove pipe when ApiResource handles NoContent
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Site has been updated');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be updated');
          this.logger.error('[SiteRegistration] SiteResource::updateSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public patchSite(siteId: number, initialSite: Site, updateSite: Site, isCompleted?: boolean): NoContent {
    const jsonPatchDoc = compare(initialSite, updateSite);

    return this.apiResource.patch<NoContent>(`sites/${siteId}`, jsonPatchDoc)
      // TODO remove pipe when ApiResource handles NoContent
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Site has been patched');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be patched');
          this.logger.error('[SiteRegistration] SiteResource::patchSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public patchLocation(locationId: number, initialLocation: Location, updateLocation: Location): NoContent {
    const jsonPatchDoc = compare(initialLocation, updateLocation);

    jsonPatchDoc.map((operation) => {
      // If mailing address is being added, change replace to add
      if (initialLocation?.physicalAddress?.city == null && updateLocation?.physicalAddress?.city != null) {
        if (operation.path.includes('physicalAddress')) {
          operation.op = 'add';
        }
      }
    });

    return this.apiResource.patch<NoContent>(`locations/${locationId}`, jsonPatchDoc)
      // TODO remove pipe when ApiResource handles NoContent
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Location has been patched');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Location could not be patched');
          this.logger.error('[SiteRegistration] SiteResource::patchLocation error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSite(siteId: number): Observable<Site> {
    return this.apiResource.delete<Site>(`sites/${siteId}`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((site: Site) => {
          this.toastService.openSuccessToast('Site has been deleted');
          this.logger.info('DELETED_SITE', site);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be deleted');
          this.logger.error('[SiteRegistration] SiteResource::deleteSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public submitSite(site: Site): Observable<string> {
    return this.apiResource.post<string>(`sites/${site.id}/submission`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site registration has been submitted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be submitted');
          this.logger.error('[SiteRegistration] SiteResource::submitSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public createBusinessLicence(siteId: number, documentGuid: string, fileName: string): Observable<BusinessLicence> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid, fileName });
    return this.apiResource.post<BusinessLicence>(`sites/${siteId}/business-licence`, { siteId }, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        tap(() => this.toastService.openSuccessToast('Business licence has been added')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence could not be added');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  // TODO should have a single GET of getBusinessLicenceById?

  public getBusinessLicences(siteId: number): Observable<BusinessLicence[]> {
    return this.apiResource.get<BusinessLicence[]>(`sites/${siteId}/business-licence`)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence[]>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicences error has occurred: ', error);
          throw error;
        })
      );
  }
}
