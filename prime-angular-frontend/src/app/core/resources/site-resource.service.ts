import { Injectable } from '@angular/core';

import { forkJoin, Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { DateUtils } from '@lib/utils/date-utils.class';
import { BusinessDay } from '@lib/models/business-day.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { PaginatedList } from '@core/models/paginated-list.model';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { CertSearch } from '@enrolment/shared/models/cert-search.model';
import { RemoteAccessSearch } from '@enrolment/shared/models/remote-access-search.model';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';

import { CommunitySiteViewModel, Site, SiteListViewModel } from '@registration/shared/models/site.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { SiteAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { IndividualDeviceProvider } from '@registration/shared/models/individual-device-provider.model';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { SiteSubmission } from '@shared/models/site-submission.model';

@Injectable({
  providedIn: 'root'
})
export class SiteResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
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

  public getPaginatedSites(
    queryParam: { textSearch?: string, careSettingCode?: CareSettingEnum, page?: number, organizationId?: number }
  ): Observable<PaginatedList<SiteRegistrationListViewModel>> {
    const params = this.apiResourceUtilsService.makeHttpParams(queryParam);
    return this.apiResource.get<PaginatedList<SiteRegistrationListViewModel>>('sites', params)
      .pipe(
        map((response: ApiHttpResponse<PaginatedList<SiteRegistrationListViewModel>>) => response.result),
        tap((organizations: PaginatedList<SiteRegistrationListViewModel>) => this.logger.info('PAGINATED_SITES', organizations)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Sites could not be retrieved');
          this.logger.error('[SiteRegistration] SiteResource::getPaginatedSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteById(siteId: number, statusCode?: number): Observable<Site> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode });
    return forkJoin({
      site: this.apiResource.get<Site>(`sites/${siteId}`, params, null, true),
      individualDeviceProviders: this.apiResource.get<IndividualDeviceProvider[]>(`sites/${siteId}/individual-device-providers`, null, null, true),
      predecessorSite: this.apiResource.get<CommunitySiteViewModel>(`sites/${siteId}/predecessor`, null, null, true)
    })
      .pipe(
        map(({ site, individualDeviceProviders, predecessorSite }) => ({ ...site, individualDeviceProviders, predecessorSite })),
        map((site: Site) => {
          site.businessHours = site.businessHours
            .map((businessDay: BusinessDay) => {
              businessDay.startTime = DateUtils.fromTimespan(businessDay.startTime);
              businessDay.endTime = DateUtils.fromTimespan(businessDay.endTime);
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

  public getSiteSubmissions(siteId: number): Observable<SiteSubmission[]> {
    return this.apiResource.get<SiteSubmission[]>(`sites/${siteId}/site-submissions`, null, null, true)
      .pipe(
        tap((siteSubmissions: SiteSubmission[]) => this.logger.info('SITE_SUBMISSIONS', siteSubmissions)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Submissions could not be retrieved');
          this.logger.error('[SiteSubmissions] getSiteSubmissions::getSiteSubmissions error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteSubmission(siteId: number, siteSubmissionId: number): Observable<SiteSubmission> {
    return this.apiResource.get<SiteSubmission>(`sites/${siteId}/site-submission/${siteSubmissionId}`, null, null, true)
      .pipe(
        tap((siteSubmission: SiteSubmission) => this.logger.info('SITE_SUBMISSION', siteSubmission)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Submission could not be retrieved');
          this.logger.error('[SiteSubmission] getSiteSubmission::getSiteSubmission error has occurred: ', error);
          throw error;
        })
      );
  }
  public getSiteContacts(siteId: number): Observable<{ label: string, email: string }[]> {
    return this.getSiteById(siteId)
      .pipe(
        map((site: Site) => [
          { label: 'Signing Authority', email: site.provisioner.email },
          ...ArrayUtils.insertIf(site?.administratorPharmaNet?.email, {
            label: 'PharmaNet Administrator',
            email: site?.administratorPharmaNet?.email
          }),
          ...ArrayUtils.insertIf(site?.privacyOfficer?.email, {
            label: 'Privacy Officer',
            email: site?.privacyOfficer?.email
          }),
          ...ArrayUtils.insertIf(site?.technicalSupport?.email, {
            label: 'Technical Support Contact',
            email: site?.technicalSupport?.email
          })
        ])
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
          businessDay.startTime = DateUtils.toTimespan(businessDay.startTime);
          businessDay.endTime = DateUtils.toTimespan(businessDay.endTime);
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

  public sendSiteReviewedEmailUser(siteId: number, note: string): NoContent {
    const payload = { data: note };
    return this.apiResource.post<NoContent>(`sites/${siteId}/site-reviewed-email`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Site reviewed notification email could not be sent');
          this.logger.error('[SiteRegistration] SiteResource::sendSiteReviewedEmailUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public updatePecCode(siteId: number, pecCode: string): NoContent {
    const payload = { data: pecCode };
    return this.apiResource.put<NoContent>(`sites/${siteId}/pec`, payload)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site ID/PEC could not be updated');
          this.logger.error('[SiteRegistration] SiteResource::updatePecCode error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateVendor(siteId: number, vendorCode: number, rationale: string): NoContent {
    const payload = { vendorCode, rationale };
    return this.apiResource.put<NoContent>(`sites/${siteId}/vendor`, payload)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Vendor has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Vendor could not be updated');
          this.logger.error('[SiteRegistration] SiteResource::updateVendor error has occurred: ', error);
          throw error;
        })
      );
  }

  public setSiteAdjudicator(siteId: number, adjudicatorId?: number): NoContent {
    const params = this.apiResourceUtilsService.makeHttpParams({ adjudicatorId });
    return this.apiResource.put<NoContent>(`sites/${siteId}/adjudicator`, null, params)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be assigned');
          this.logger.error('[Adjudication] AdjudicationResource::setSiteAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeSiteAdjudicator(siteId: number): NoContent {
    return this.apiResource.delete<NoContent>(`sites/${siteId}/adjudicator`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be unassigned');
          this.logger.error('[Adjudication] AdjudicationResource::removeSiteAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSite(siteId: number): NoContent {
    return this.apiResource.delete<NoContent>(`sites/${siteId}`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be deleted');
          this.logger.error('[SiteRegistration] SiteResource::deleteSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public submitSite(siteId: number, site: Site & { businessLicence: { documentGuid: string } }): NoContent {
    if (site.businessHours?.length) {
      site.businessHours = site.businessHours
        .map((businessDay: BusinessDay) => {
          businessDay.startTime = DateUtils.toTimespan(businessDay.startTime);
          businessDay.endTime = DateUtils.toTimespan(businessDay.endTime);
          return businessDay;
        });
    } else {
      site.businessHours = null;
    }
    return this.apiResource.post<NoContent>(`sites/${siteId}/submissions`, site)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been submitted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be submitted');
          this.logger.error('[SiteRegistration] SiteResource::submitSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public createBusinessLicence(siteId: number, businessLicence: BusinessLicence, documentGuid: string): Observable<BusinessLicence> {
    const params = documentGuid ? this.apiResourceUtilsService.makeHttpParams({ documentGuid }) : null;
    return this.apiResource.post<BusinessLicence>(`sites/${siteId}/business-licences`, businessLicence, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateBusinessLicence(siteId: number, businessLicence: BusinessLicence): Observable<BusinessLicence> {
    return this.apiResource.put<BusinessLicence>(`sites/${siteId}/business-licences/${businessLicence.id}`, businessLicence)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::updateBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  public createBusinessLicenceDocument(
    siteId: number,
    businessLicenceId: number,
    documentGuid: string
  ): Observable<BusinessLicenceDocument> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid });
    return this.apiResource.post<BusinessLicenceDocument>(`sites/${siteId}/business-licences/${businessLicenceId}/document`, null, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicenceDocument>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::createBusinessLicenceDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeBusinessLicenceDocument(siteId: number, businessLicenceId: number): NoContent {
    return this.apiResource.delete<BusinessLicenceDocument>(`sites/${siteId}/business-licences/${businessLicenceId}/document`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteRegistrationResource::removeBusinessLicenceDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBusinessLicence(siteId: number): Observable<BusinessLicence> {
    return this.apiResource.get<BusinessLicence>(`sites/${siteId}/business-licences?latest=true`)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licence could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicence error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBusinessLicences(siteId: number): Observable<BusinessLicence[]> {
    return this.apiResource.get<BusinessLicence[]>(`sites/${siteId}/business-licences`)
      .pipe(
        map((response: ApiHttpResponse<BusinessLicence[]>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Business Licences could not be Retrieved');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::getBusinessLicences error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBusinessLicenceDocumentToken(siteId: number, businessLicenceId: number): Observable<string> {
    return this.apiResource.get<string>(`sites/${siteId}/business-licences/${businessLicenceId}/document/token`)
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

  public approveSite(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/approve`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been approved')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be approved');
          this.logger.error('[SiteRegistration] SiteResource::approveSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public rejectSite(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/decline`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been declined')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be declined');
          this.logger.error('[SiteRegistration] SiteResource::declineSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public enableEditingSite(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/enable-editing`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site editing has been enabled')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration editing could not be enabled');
          this.logger.error('[SiteRegistration] SiteResource::enableEditingSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public unrejectSite(siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`sites/${siteId}/unreject`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site has been unrejected')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site registration could not be unrejected');
          this.logger.error('[SiteRegistration] SiteResource::unrejectSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public flagSite(siteId: number, flagged: boolean): NoContent {
    const url = `sites/${siteId}/flag`;
    const body = { data: flagged };
    const request$ = this.apiResource.put<NoContent>(url, body);

    return request$
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast(`Site has been ${flagged ? 'flagged' : 'unflagged'}`)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site flag could not be updated');
          this.logger.error('[Site] SiteResource::flagSite error has occurred:'
            , error);
          throw error;
        })
      );
  }

  public flagIsNewSite(siteId: number, isNew: boolean): NoContent {
    const url = `sites/${siteId}/isnew`;
    const body = { data: isNew };
    const request$ = this.apiResource.put<NoContent>(url, body);

    return request$
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast(`Site has been ${isNew ? ' flagged is-new' : 'unflagged is-new'}`)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site is-new could not be updated');
          this.logger.error('[Site] SiteResource::flagIsNewSite error has occurred:'
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

  public pecAssignable(siteId: number, pec: string): Observable<boolean> {
    return this.apiResource.post(`sites/${siteId}/pec/${pec}/assignable`)
      .pipe(
        map((response: ApiHttpResponse<boolean>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::pecAssignable error has occurred: ', error);
          throw error;
        })
      );
  }

  public pecExistsWithinHa(siteId: number, pec: string): Observable<boolean> {
    return this.apiResource.get(`sites/${siteId}/pec/${pec}/exists-within-ha`)
      .pipe(
        map((response: ApiHttpResponse<boolean>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::pecExistsWithinHa error has occurred: ', error);
          throw error;
        })
      );
  }

  public archiveSite(siteId: number, note: string): NoContent {
    return this.apiResource.post<NoContent>(`sites/${siteId}/archive`, { note })
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::archiveSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public restoreArchivedSite(siteId: number, note: string): NoContent {
    return this.apiResource.post<NoContent>(`sites/${siteId}/restore`, { note })
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::restoreArchivedSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public canRestoreSite(siteId: number): Observable<boolean> {
    return this.apiResource.get<boolean>(`sites/${siteId}/can-restore`)
      .pipe(
        map((response: ApiHttpResponse<boolean>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::canRestoreSite error has occurred: ', error);
          throw error;
        })
      );
  }

  public saveSiteLink(predecessorSiteId: number, successorSiteId: number): NoContent {
    return this.apiResource.post<boolean>(`sites/${successorSiteId}/link/${predecessorSiteId}/add`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::saveSiteLink error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeSiteLink(predecessorSiteId: number, successorSiteId: number): NoContent {
    return this.apiResource.post<boolean>(`sites/${successorSiteId}/link/${predecessorSiteId}/remove`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::removeSiteLink error has occurred: ', error);
          throw error;
        })
      );
  }

  public getPredecessorSite(siteId: number): Observable<Site> {
    return this.apiResource.get<Site>(`sites/${siteId}/predecessor`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        catchError((error: any) => {
          this.logger.error('[SiteRegistration] SiteResource::getPredecessorSite error has occurred: ', error);
          throw error;
        })
      );
  }
}
