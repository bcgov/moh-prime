import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';

import { CertSearch } from '@enrolment/shared/models/cert-search.model';
import { RemoteAccessSearch } from '@enrolment/shared/models/remote-access-search.model';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';

import { BusinessDay } from '@registration/shared/models/business-day.model';
import { Site, SiteListViewModel } from '@registration/shared/models/site.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { BusinessDayHours } from '@registration/shared/models/business-day-hours.model';
import { SiteAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';

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
  public getSites(organizationId: number, queryParams: { verbose: boolean; }): Observable<SiteListViewModel[] | Site[]>;
  public getSites(organizationId: number, queryParams: { verbose: boolean; } = null): Observable<SiteListViewModel[] | Site[]> {
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

  public getSiteById(siteId: number, statusCode?: number): Observable<Site> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode });
    return this.apiResource.get<Site>(`sites/${siteId}`, params)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        map((site: Site) => {
          site.businessHours = site.businessHours
            .map((businessDay: BusinessDay) => {
              businessDay.startTime = BusinessDayHours.fromTimeSpan(businessDay.startTime);
              businessDay.endTime = BusinessDayHours.fromTimeSpan(businessDay.endTime);
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
    if (site.businessHours?.length) {
      site.businessHours = site.businessHours
        .map((businessDay: BusinessDay) => {
          businessDay.startTime = BusinessDayHours.toTimespan(businessDay.startTime);
          businessDay.endTime = BusinessDayHours.toTimespan(businessDay.endTime);
          return businessDay;
        });
    } else {
      site.businessHours = null;
    }
    return this.apiResource.put<NoContent>(`sites/${site.id}`, site)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be updated');
          this.logger.error('[SiteRegistration] SiteResource::updateSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public setSiteCompleted(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/completed`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::setCompleted error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeSiteCompleted(siteId: number): NoContent {
    return this.apiResource.delete<NoContent>(`sites/${siteId}/completed`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::removeCompleted error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendRemoteUsersEmailAdmin(siteId: number): NoContent {
    return this.apiResource.post<NoContent>(`sites/${siteId}/remote-users-email-admin`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Remote users update email could not be sent');
          this.logger.error('[SiteRegistration] SiteResource::sendRemoteUsersEmailAdmin error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendRemoteUsersEmailUser(siteId: number, newRemoteUsers: RemoteUser[]): NoContent {
    return this.apiResource.post<NoContent>(`sites/${siteId}/remote-users-email-user`, newRemoteUsers)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Remote users email could not be sent');
          this.logger.error('[SiteRegistration] SiteResource::sendRemoteUsersEmailUser error has occurred: ', error);
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

  public setSiteAdjudicator(siteId: number, adjudicatorId?: number): Observable<Site> {
    const params = this.apiResourceUtilsService.makeHttpParams({ adjudicatorId });
    return this.apiResource.put<Site>(`sites/${siteId}/adjudicator`, null, params)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        map((site: Site) => site),
        tap((site: Site) => this.logger.info('UPDATED_SITE', site)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be assigned');
          this.logger.error('[Adjudication] AdjudicationResource::setSiteAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeSiteAdjudicator(siteId: number): Observable<Site> {
    return this.apiResource.delete<Site>(`sites/${siteId}/adjudicator`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        map((site: Site) => site),
        tap((site: Site) => this.logger.info('UPDATED_SITE', site)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be unassigned');
          this.logger.error('[Adjudication] AdjudicationResource::removeSiteAdjudicator error has occurred: ', error);
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

  public createBusinessLicence(siteId: number, businessLicence: BusinessLicence, documentGuid: string): Observable<BusinessLicence> {
    const params = documentGuid ? this.apiResourceUtilsService.makeHttpParams({ documentGuid }) : null;
    return this.apiResource.post<BusinessLicence>(`sites/${siteId}/business-licence`, businessLicence, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateBusinessLicence(siteId: number, businessLicence: BusinessLicence): Observable<BusinessLicence> {
    return this.apiResource.put<BusinessLicence>(`sites/${siteId}/business-licence`, businessLicence)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::updateBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  public createBusinessLicenceDocument(siteId: number, documentGuid: string): Observable<BusinessLicenceDocument> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid });
    return this.apiResource.post<BusinessLicenceDocument>(`sites/${siteId}/business-licence/document`, null, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicenceDocument>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createBusinessLicenceDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeBusinessLicenceDocument(siteId: number): NoContent {
    return this.apiResource.delete<BusinessLicenceDocument>(`sites/${siteId}/business-licence/document`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::removeBusinessLicenceDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBusinessLicence(siteId: number): Observable<BusinessLicence> {
    return this.apiResource.get<BusinessLicence>(`sites/${siteId}/business-licence`)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicences error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBusinessLicenceDocumentToken(siteId: number): Observable<string> {
    return this.apiResource.get<string>(`sites/${siteId}/business-licence/document/token`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence token could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicenceDocumentToken error has occurred: ', error);
          throw error;
        })
      );
  }

  public createSiteAdjudicationDocument(siteId: number, documentGuid: string): Observable<SiteAdjudicationDocument> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid });
    return this.apiResource.post<SiteAdjudicationDocument>(`sites/${siteId}/adjudication-documents`, { siteId }, params)
      .pipe(
        map((response: ApiHttpResponse<SiteAdjudicationDocument>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createSiteAdjudicationDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteAdjudicationDocuments(siteId: number): Observable<SiteAdjudicationDocument[]> {
    return this.apiResource.get<SiteAdjudicationDocument[]>(`sites/${siteId}/adjudication-documents`)
      .pipe(
        map((response: ApiHttpResponse<SiteAdjudicationDocument[]>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Adjudication Documents could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getSiteAdjudicationDocuments error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSiteAdjudicationDocument(siteId: number, documentId: number) {
    return this.apiResource.delete<SiteAdjudicationDocument>(
      `sites/${siteId}/adjudication-documents/${documentId}`)
      .pipe(
        map((response: ApiHttpResponse<SiteAdjudicationDocument>) => response.result),
        map((document: SiteAdjudicationDocument) => document),
        tap((document: SiteAdjudicationDocument) => {
          this.toastService.openSuccessToast('Document has been deleted');
          this.logger.info('DELETED_DOCUMENT', document);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Document could not be deleted');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::deleteSiteAdjudicationDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteAdjudicationDocumentDownloadToken(siteId: number, documentId: number): Observable<string> {
    return this.apiResource.get<string>(`sites/${siteId}/adjudication-documents/${documentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Adjudication Document token could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getSiteAdjudicationDocumentDownloadToken error has occurred: ',
            error);
          throw error;
        })
      );
  }

  public approveSite(siteId: number): Observable<Site> {
    return this.apiResource.put<Site>(`sites/${siteId}/approve`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site registration has been approved')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be approved');
          this.logger.error('[SiteRegistration] SiteResource::approveSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public declineSite(siteId: number): Observable<Site> {
    return this.apiResource.put<Site>(`sites/${siteId}/decline`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site registration has been declined')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be declined');
          this.logger.error('[SiteRegistration] SiteResource::declineSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public enableEditingSite(siteId: number): Observable<Site> {
    return this.apiResource.put<Site>(`sites/${siteId}/enable-editing`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site registration editing has been enabled')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration editing could not be enabled');
          this.logger.error('[SiteRegistration] SiteResource::enableEditingSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public unrejectSite(siteId: number): Observable<Site> {
    return this.apiResource.put<Site>(`sites/${siteId}/unreject`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site has been unrejected')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be unrejected');
          this.logger.error('[SiteRegistration] SiteResource::unrejectSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public flagSite(siteId: number, flagged: boolean): Observable<Site> {
    const url = `sites/${siteId}/flag/${flagged}`;
    const request$ = this.apiResource.put<Site>(url, null);

    return request$
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        map((site: Site) => site),
        tap((site: Site) => this.logger.info('UPDATED SITE', site)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site flag could not be updated');
          this.logger.error('[Site] SiteResource::flagSite error has occurred:'
            , error);
          throw error;
        })
      );
  }

  public createSiteRegistrationNote(siteId: number, note: string): Observable<SiteRegistrationNote> {
    const payload = { data: note };
    return this.apiResource.post(`sites/${siteId}/site-registration-notes`, payload)
      .pipe(
        map((response: ApiHttpResponse<SiteRegistrationNote>) => response.result),
        tap((adjudicatorNote: SiteRegistrationNote) => {
          this.toastService.openErrorToast('Site Registration Note has been saved');
          this.logger.info('NEW_SITE_REGISTRATION_NOTE', adjudicatorNote);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Registration note could not be saved');
          this.logger.error('[SiteRegistration] SiteResource::createSiteRegistrationNote error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteRegistrationNotes(siteId: number): Observable<SiteRegistrationNote[]> {
    return this.apiResource.get(`sites/${siteId}/site-registration-notes`)
      .pipe(
        map((response: ApiHttpResponse<SiteRegistrationNote[]>) => response.result),
        tap((siteRegistrationNotes: SiteRegistrationNote[]) => this.logger.info('SITE_REGISTRATION_NOTES', siteRegistrationNotes)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Registration notes could not be retrieved');
          this.logger.error('[SiteRegistration] SiteResource::getSiteRegistrationNotes error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSitesByRemoteUserInfo(certSearch: CertSearch[]): Observable<RemoteAccessSearch[]> {
    return this.apiResource.post(`sites/remote-users`, certSearch)
      .pipe(
        map((response: ApiHttpResponse<RemoteAccessSearch[]>) => response.result),
        tap((sites: RemoteAccessSearch[]) => this.logger.info('SITES', sites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Sites could not be retrieved');
          this.logger.error('[SiteRegistration] SiteResource::getSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteBusinessEvents(siteId: number, businessEventTypeCodes: BusinessEventTypeEnum[]): Observable<BusinessEvent[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ businessEventTypeCodes });
    return this.apiResource.get<BusinessEvent[]>(`sites/${siteId}/events`, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessEvent[]>) => response.result),
        tap((businessEvents: BusinessEvent[]) =>
          this.logger.info('SITE_BUSINESS_EVENTS', businessEvents)
        ),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site business events could not be retrieved');
          this.logger.error('[SiteRegistration] SiteResource::getSiteBusinessEvents error has occurred: ', error);
          throw error;
        })
      );
  }
}
