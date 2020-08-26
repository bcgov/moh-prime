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

import { Site, SiteListViewModel } from '@registration/shared/models/site.model';
import { BusinessLicenceDocument } from '../../modules/site-registration/shared/models/business-licence-document.model';

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

  public getSites(organizationId: number): Observable<SiteListViewModel[]>;
  public getSites(organizationId: number, queryParams: { verbose: boolean }): Observable<SiteListViewModel[] | Site[]>;
  public getSites(organizationId: number, queryParams: { verbose: boolean } = null): Observable<SiteListViewModel[] | Site[]> {
    const params = this.apiResourceUtilsService.makeHttpParams(queryParams);
    return this.apiResource.get<SiteListViewModel[] | Site[]>(`organizations/${organizationId}/sites`, params)
      .pipe(
        map((response: ApiHttpResponse<SiteListViewModel[] | Site[]>) => response.result),
        tap((sites: SiteListViewModel[] | Site[]) => this.logger.info('SITES', sites)),
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
          site.businessHours = site.businessHours.map((businessDay: BusinessDay) => {
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

  public updateSite(site: Site): NoContent {
    // TODO separate this out into a proper adapter
    if (site.businessHours?.length) {
      site.businessHours = site.businessHours
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
      site.businessHours = null;
    }

    return this.apiResource.put<NoContent>(`sites/${site.id}`, site)
      .pipe(
        // TODO remove pipe when ApiResource handles NoContent
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

  public updateCompleted(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/completed`)
      .pipe(
        // TODO remove pipe when ApiResource handles NoContent
        map(() => { }),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::updateSiteCompleted error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendRemoteUsersEmail(siteId: number): NoContent {
    return this.apiResource.post<NoContent>(`sites/${siteId}/remote-users-email`)
      .pipe(
        map(() => { }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Remote users update email could not be sent');
          this.logger.error('[SiteRegistration] SiteResource::sendRemoteUsersEmail error has occurred: ', error);
          throw error;
        })
      );
  }

  public updatePecCode(siteId: number, pecCode: string): Observable<Site> {
    const payload = { data: pecCode };
    return this.apiResource.put<Site>(`sites/${siteId}/pec`, payload)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((site: Site) => {
          this.toastService.openSuccessToast('Site has been updated');
          this.logger.info('UPDATED_SITE', site);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be updated');
          this.logger.error('[SiteRegistration] SiteResource::updatePecCode error has occurred: ', error);
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

  public createBusinessLicence(siteId: number, documentGuid: string, filename: string): Observable<BusinessLicenceDocument> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid, filename });
    return this.apiResource.post<BusinessLicenceDocument>(`sites/${siteId}/business-licence`, { siteId }, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicenceDocument>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  // TODO should have a single GET of getBusinessLicenceById?

  public getBusinessLicences(siteId: number): Observable<BusinessLicenceDocument[]> {
    return this.apiResource.get<BusinessLicenceDocument[]>(`sites/${siteId}/business-licence`)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicenceDocument[]>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicences error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBusinessLicenceDownloadToken(siteId: number): Observable<string> {
    return this.apiResource.get<string>(`sites/${siteId}/latest-business-licence`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence token could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicenceDownloadToken error has occurred: ', error);
          throw error;
        })
      );
  }
}
