import { Injectable } from '@angular/core';


import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import * as moment from 'moment';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { NoContent } from '@core/resources/abstract-resource';
import { BusinessDay } from '@lib/modules/business-hours/models/business-day.model';

import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';

// TODO use ApiResourceUtils to build URLs
// TODO split out log messages for reuse into ErrorHandler
@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public getSites() {
    return this.apiResource.get<Site[]>('sites')
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
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteById(siteId: number) {
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
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getSiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createSite(party: Party) {
    return this.apiResource.post<Site>('sites', party)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((newSite: Site) => {
          this.toastService.openSuccessToast('Site has been created');
          this.logger.info('NEW_SITE', newSite);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be created');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createSite error has occurred: ', error);
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
          this.logger.error('[SiteRegistration] SiteRegistrationResource::updateSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSite(siteId: number) {
    return this.apiResource.delete<Site>(`sites/${siteId}`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((site: Site) => {
          this.toastService.openSuccessToast('Site has been deleted');
          this.logger.info('DELETED_SITE', site);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be deleted');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::deleteSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationAgreement(): Observable<string> {
    return this.apiResource.get<string>(`sites/organization-agreement`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSignedOrganizationAgreement(siteId: number): Observable<string> {
    return this.apiResource.get<string>(`sites/${siteId}/organization-agreement`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getCurrentOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public acceptCurrentOrganizationAgreement(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/organization-agreement`)
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Organization agreement has been accepted');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be accepted');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::acceptOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public submitSiteRegistration(site: Site): Observable<string> {
    return this.apiResource.post<string>(`sites/${site.id}/submission`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site registration has been submitted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be submitted');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::submitSiteRegistration error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get the organization information from OrgBook based on a business number.
   *
   * @param businessNumber Registered business number (BN15) assigned to the organization.
   */
  public getOrganizationInfo(businessNumber: string): Observable<any> {
    return this.apiResource.get<any>(`sites/organization/${businessNumber}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap(() => this.toastService.openSuccessToast('Organization information has been applied to site registration')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization information could not be found');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getOrganizationInfo error has occurred: ', error);
          throw error;
        })
      );
  }
}
