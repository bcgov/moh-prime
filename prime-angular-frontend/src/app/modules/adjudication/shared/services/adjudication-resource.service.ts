import { EnrolleeNavigation } from './../../../../shared/models/enrollee-navigation-model';
import { Injectable } from '@angular/core';

import { forkJoin, Observable, of } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import moment from 'moment';

import { Admin, AdminUser } from '@auth/shared/models/admin.model';
import { ObjectUtils } from '@lib/utils/object-utils.class';
import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { PaginatedList } from '@core/models/paginated-list.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { EnrolleeStatusAction } from '@shared/enums/enrollee-status-action.enum';
import { EnrolleeAgreement } from '@shared/models/agreement.model';
import { HttpEnrolleeSubmission } from '@shared/models/enrollee-submission.model';
import { HttpEnrollee, EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolmentStatusReference } from '@shared/models/enrolment-status-reference.model';
import { EnrolmentCard } from '@shared/models/enrolment-card.model';
import { BulkEmailType } from '@shared/enums/bulk-email-type';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { AgreementVersion } from '@shared/models/agreement-version.model';
import { EnrolleeReviewStatus } from '@shared/models/enrollee-review-status.model';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';

import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { PlrInfo } from '@adjudication/shared/models/plr-info.model';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { EnrolleeNotification } from '../models/enrollee-notification.model';
import { SiteNotification } from '../models/site-notification.model';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { EnrolleeDeviceProvider } from '@shared/models/enrollee-device-provider.model';
import { AdminStatusType } from '../models/admin-status.enum';
import { OrganizationAdminView } from '@registration/shared/models/organization.model';

@Injectable({
  providedIn: 'root'
})
export class AdjudicationResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public getEnrollees(params: {
    textSearch?: string,
    statusCode?: number,
    isLinkedPaperEnrolment?: boolean,
    isRenewedManualEnrolment?: boolean,
    page?: number,
    sortOrder?: string,
    assignedTo?: number,
    appliedDateStart?: string,
    appliedDateEnd?: string,
    renewalDateStart?: string,
    renewalDateEnd?: string
  }): Observable<PaginatedList<EnrolleeListViewModel>> {
    const httpParams = this.apiResourceUtilsService.makeHttpParams(params);
    return this.apiResource.get<PaginatedList<EnrolleeListViewModel>>('enrollees', httpParams)
      .pipe(
        map((response: ApiHttpResponse<PaginatedList<EnrolleeListViewModel>>) => response.result),
        tap((enrollees: PaginatedList<EnrolleeListViewModel>) => this.logger.info('PAGINATED_ENROLLEES', enrollees)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolments could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrollees error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeById(enrolleeId: number): Observable<HttpEnrollee> {
    return forkJoin({
      enrollee: this.apiResource.get<HttpEnrollee>(`enrollees/${enrolleeId}`)
        .pipe(map((response: ApiHttpResponse<HttpEnrollee>) => response.result)),
      enrolleeCareSettings: this.apiResource.get<CareSetting>(`enrollees/${enrolleeId}/care-settings`)
        .pipe(map((response: ApiHttpResponse<CareSetting>) => response.result)),
      certifications: this.apiResource.get<CollegeCertification[]>(`enrollees/${enrolleeId}/certifications`)
        .pipe(map((response: ApiHttpResponse<CollegeCertification[]>) => response.result)),
      enrolleeDeviceProviders: this.apiResource.get<EnrolleeDeviceProvider[]>(`enrollees/${enrolleeId}/device-providers`)
        .pipe(map((response: ApiHttpResponse<EnrolleeDeviceProvider[]>) => response.result)),
      unlistedCertifications: this.apiResource.get<UnlistedCertification[]>(`enrollees/${enrolleeId}/unlisted-certifications`)
        .pipe(map((response: ApiHttpResponse<UnlistedCertification[]>) => response.result)),
      enrolleeRemoteUsers: this.apiResource.get<EnrolleeRemoteUser[]>(`enrollees/${enrolleeId}/remote-users`)
        .pipe(map((response: ApiHttpResponse<EnrolleeRemoteUser[]>) => response.result)),
      oboSites: this.apiResource.get<OboSite[]>(`enrollees/${enrolleeId}/obo-sites`)
        .pipe(map((response: ApiHttpResponse<OboSite[]>) => response.result)),
      remoteAccessLocations: this.apiResource.get<RemoteAccessLocation[]>(`enrollees/${enrolleeId}/remote-locations`)
        .pipe(map((response: ApiHttpResponse<RemoteAccessLocation[]>) => response.result)),
      remoteAccessSites: this.apiResource.get<RemoteAccessSite[]>(`enrollees/${enrolleeId}/remote-sites`)
        .pipe(map((response: ApiHttpResponse<RemoteAccessSite[]>) => response.result)),
      selfDeclarations: this.apiResource.get<SelfDeclaration[]>(`enrollees/${enrolleeId}/self-declarations`)
        .pipe(map((response: ApiHttpResponse<SelfDeclaration[]>) => response.result)),
      selfDeclarationDocuments: this.apiResource.get<SelfDeclarationDocument[]>(`enrollees/${enrolleeId}/self-declarations/documents`)
        .pipe(map((response: ApiHttpResponse<SelfDeclarationDocument[]>) => response.result)),
      adjudicatorIdir: this.apiResource.get<string>(`enrollees/${enrolleeId}/adjudicator-idir`)
        .pipe(map((response: ApiHttpResponse<string>) => response.result))
    })
      .pipe(
        map(({ enrollee, enrolleeCareSettings, ...remainder }) => {
          return { ...enrollee, ...enrolleeCareSettings, ...remainder }
        }),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getPlrInfoByEnrolleeId(enrolleeId: number): Observable<PlrInfo[]> {
    return this.apiResource.get<PlrInfo[]>(`enrollees/${enrolleeId}/plrs`)
      .pipe(
        map((response: ApiHttpResponse<PlrInfo[]>) => response.result),
        tap((plrs: PlrInfo[]) => this.logger.info('PLRS', plrs)),
        catchError((error: any) => {
          this.toastService.openErrorToast('PLR data could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getPlrInfoByEnrolleeId error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdjacentEnrolleeId(enrolleeId: number): Observable<EnrolleeNavigation> {
    return this.apiResource.get<EnrolleeNavigation>(`enrollees/${enrolleeId}/adjacent`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNavigation>) => response.result),
        tap((enrolleeNaviagation: EnrolleeNavigation) => this.logger.info('ENROLLEE_NAVIGATION', enrolleeNaviagation)),
        catchError((error: any) => {
          this.toastService.openErrorToast('EnrolleeNaviagation could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getAdjacentEnrolleeId error has occurred: ', error);
          throw error;
        })
      );
  }

  public enrolleeStatusAction(enrolleeId: number, action: EnrolleeStatusAction): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>(`enrollees/${enrolleeId}/status-actions/${action}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => {
          this.toastService.openErrorToast('Enrolment status has been updated');
          this.logger.info('UPDATED_ENROLLEE', enrollee);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be updated');
          this.logger.error('[Adjudication] AdjudicationResource::enrolleeStatusAction error has occurred: ', error);
          throw error;
        })
      );
  }

  public setEnrolleeAdjudicator(enrolleeId: number, adjudicatorId: number): Observable<string> {
    const params = this.apiResourceUtilsService.makeHttpParams({ adjudicatorId });
    return this.apiResource.put<string>(`enrollees/${enrolleeId}/adjudicator`, null, params)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be assigned');
          this.logger.error('[Adjudication] AdjudicationResource::setEnrolleeAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeEnrolleeAdjudicator(enrolleeId: number): NoContent {
    return this.apiResource.delete<NoContent>(`enrollees/${enrolleeId}/adjudicator`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be unassigned');
          this.logger.error('[Adjudication] AdjudicationResource::removeEnrolleeAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEnrolleeAlwaysManual(enrolleeId: number, alwaysManual: boolean): NoContent {
    const url = `enrollees/${enrolleeId}/always-manual`;
    const request$ = (alwaysManual)
      ? this.apiResource.put<NoContent>(url, null)
      : this.apiResource.delete<NoContent>(url);

    return request$
      .pipe(
        NoContentResponse,
        tap(() => this.logger.info('UPDATED_ENROLLEE', alwaysManual)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be marked as always manual');
          this.logger.error('[Adjudication] AdjudicationResource::updateEnrolleeAlwaysManual error has occurred: ', error);
          throw error;
        })
      );
  }

  public confirmSubmission(enrolleeId: number): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/submissions/latest/confirm`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee submission could not be confirmed');
          this.logger.error('[Adjudication] AdjudicationResource::confirmSubmission error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeBusinessEvents(enrolleeId: number, businessEventTypeCodes: BusinessEventTypeEnum[]): Observable<BusinessEvent[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ businessEventTypeCodes });
    return this.apiResource.get<BusinessEvent[]>(`enrollees/${enrolleeId}/events`, params)
      .pipe(
        map((response: ApiHttpResponse<BusinessEvent[]>) => response.result),
        tap((businessEvents: BusinessEvent[]) =>
          this.logger.info('ENROLLEE_BUSINESS_EVENTS', businessEvents)
        ),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee business events could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeBusinessEvents error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteEnrollee(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${enrolleeId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => {
          this.toastService.openSuccessToast('Enrolment has been deleted');
          this.logger.info('DELETED_ENROLLEE', enrollee);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be deleted');
          this.logger.error('[Adjudication] AdjudicationResource::deleteEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendEnrolleeReminderEmail(enrolleeId: number): NoContent {
    return this.apiResource.post<NoContent>(`enrollees/${enrolleeId}/reminder`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openErrorToast('Enrollee reminder has been sent')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee reminder could not be sent');
          this.logger.error('[Enrolment] EnrolmentResource::sendReminderEmail error has occurred: ', error);
          throw error;
        })
      );
  }

  public createInitiatedEnrolleeEmailEvent(enrolleeId: number): NoContent {
    return this.apiResource.post<NoContent>(`enrollees/${enrolleeId}/events/email-initiated`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openErrorToast('Enrollee initiated email event has been created')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee initiated email event could not be created');
          this.logger.error('[Enrolment] EnrolmentResource::createInitiatedEnrolleeEmailEvent error has occurred: ', error);
          throw error;
        })
      );

  }

  public getAdjudicatorNotes(enrolleeId: number): Observable<EnrolleeNote[]> {
    return this.apiResource.get(`enrollees/${enrolleeId}/adjudicator-notes`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote[]>) => response.result),
        tap((adjudicatorNotes: EnrolleeNote[]) => this.logger.info('ADJUDICATOR_NOTES', adjudicatorNotes)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator notes could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getAdjudicatorNotes error has occurred: ', error);
          throw error;
        })
      );
  }

  public createAdjudicatorNote(enrolleeId: number, note: string, link: boolean = false): Observable<EnrolleeNote> {
    const payload = { data: note };
    const params = this.apiResourceUtilsService.makeHttpParams({ link });
    return this.apiResource.post(`enrollees/${enrolleeId}/adjudicator-notes`, payload, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote>) => response.result),
        tap((adjudicatorNote: EnrolleeNote) => {
          this.toastService.openErrorToast('Adjudication note has been saved');
          this.logger.info('NEW_ADJUDICATOR_NOTE', adjudicatorNote);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudication note could not be saved');
          this.logger.error('[Adjudication] AdjudicationResource::createAdjudicatorNote error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAccessAgreementNote(
    enrolleeId: number,
    note: string
  ): Observable<EnrolleeNote> {
    const payload = { enrolleeId, note };
    return this.apiResource.put(`enrollees/${enrolleeId}/access-agreement-notes`, payload)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote>) => response.result),
        tap((adjudicatorNote: EnrolleeNote) => {
          this.toastService.openSuccessToast(`Limits and conditions clause has been saved.`);
          this.logger.info('LIMITS_AND_CONDITIONS_CLAUSE', adjudicatorNote);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Limits and conditions clause could not be updated');
          this.logger.error('[Adjudication] AdjudicationResource::updateAccessAgreementNote error has occurred: ', error);
          throw error;
        })
      );
  }

  public changeAgreementType(enrolleeId: number, note: string, agreementType: number): Observable<EnrolleeNote> {
    const payload = { note, agreementType };
    return this.apiResource.put(`enrollees/${enrolleeId}/status-actions/change-toa`, payload)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote>) => response.result),
        tap(() => {
          this.toastService.openSuccessToast(`Agreement type changed.`);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Agreement type could not be changed');
          this.logger.error('[Adjudication] AdjudicationResource::changeAgreementType error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAccessAgreementNote(enrolleeId: number): Observable<EnrolleeNote> {
    return this.apiResource.get<EnrolleeNote>(`enrollees/${enrolleeId}/access-agreement-notes`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote>) => response.result),
        tap((adjudicatorNotes: EnrolleeNote) => this.logger.info('LIMITS_AND_CONDITIONS_CLAUSE', adjudicatorNotes)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getAccessAgreementNote error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeReviewStatus(enrolleeId: number): Observable<EnrolleeReviewStatus> {
    return forkJoin({
      selfDeclarationDocuments: this.apiResource.get<SelfDeclarationDocument[]>(`enrollees/${enrolleeId}/self-declarations/documents`)
        .pipe(map((response: ApiHttpResponse<SelfDeclarationDocument[]>) => response.result)),
      enrolmentStatuses: this.apiResource.get<EnrolmentStatus[]>(`enrollees/${enrolleeId}/statuses`)
        .pipe(map((response: ApiHttpResponse<EnrolmentStatus[]>) => response.result)),
      // IdentificationDocument is not currently pupulated in backend
      identificationDocuments: of([])
    })
      .pipe(
        tap((reviewStatus: EnrolleeReviewStatus) => this.logger.info('ENROLLEE_REVIEW_STATUS', reviewStatus)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee review status could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeReviewStatus error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Agreements
  // ---

  public getLatestAgreementVersions(type?: AgreementTypeGroup): Observable<AgreementVersion[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ type });
    return this.apiResource.get<AgreementVersion[]>('agreements', params)
      .pipe(
        map((response: ApiHttpResponse<AgreementVersion[]>) => response.result),
        tap((agreementVersions: AgreementVersion[]) => this.logger.info('AGREEMENT_VERSIONS', agreementVersions)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Agreement versions could not be found.');
          this.logger.error('[Adjudication] AdjudicationResource::getLatestAgreementVersions error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAgreementVersion(agreementVersionId: number): Observable<AgreementVersion> {
    return this.apiResource.get<AgreementVersion>(`agreements/${agreementVersionId}`)
      .pipe(
        map((response: ApiHttpResponse<AgreementVersion>) => response.result),
        tap((agreementVersion: AgreementVersion) => this.logger.info('AGREEMENT_VERSION', agreementVersion)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Agreement version could not be found.');
          this.logger.error('[Adjudication] AdjudicationResource::getAgreementVersion error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAcceptedAccessTermsByYear(enrolleeId: number, yearAccepted: number): Observable<EnrolleeAgreement[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ yearAccepted });
    return this.apiResource.get<EnrolleeAgreement[]>(`enrollees/${enrolleeId}/agreements`, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement[]>) => response.result),
        tap((accessTerms: EnrolleeAgreement[]) => this.logger.info('ENROLLEE_AGREEMENT', accessTerms)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee agreements could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getAccessTerms error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentCardsByYear(enrolleeId: number, yearAccepted: number): Observable<EnrolmentCard[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ yearAccepted });
    return this.apiResource.get<EnrolmentCard[]>(`enrollees/${enrolleeId}/cards`, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCard[]>) => response.result),
        tap((accessTerms: EnrolmentCard[]) => this.logger.info('ENROLMENT_CARDS', accessTerms)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment cards could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolmentCardsByYear error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAccessTerm(enrolleeId: number, agreementId: number): Observable<EnrolleeAgreement> {
    return this.apiResource.get(`enrollees/${enrolleeId}/agreements/${agreementId}`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement>) => response.result),
        tap((accessTerm: EnrolleeAgreement) => this.logger.info('ACCESS_TERM', accessTerm)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getAccessTerm error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSubmissionForAgreement(enrolleeId: number, agreementId: number)
    : Observable<HttpEnrolleeSubmission> {
    return this.apiResource.get(`enrollees/${enrolleeId}/agreements/${agreementId}/submission`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeSubmission>) => response.result),
        tap((enrolleeSubmission: HttpEnrolleeSubmission) =>
          this.logger.info('ENROLLEE_SUBMISSION', enrolleeSubmission)
        ),
        map(this.enrolleeSubmissionAdapterResponse()),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getSubmissionForAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Assign a TOA agreement to a enrollee that is under review.
   */
  public assignToaAgreementType(enrolleeId: number, agreementType: AgreementType): Observable<HttpEnrollee> {
    return this.apiResource.put<HttpEnrollee>(`enrollees/${enrolleeId}/submissions/latest/type`, agreementType)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap(() => this.toastService.openSuccessToast('TOA agreement has been assigned')),
        tap((enrollee: HttpEnrollee) => this.logger.info('UPDATED_ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('TOA agreement could not be assigned.');
          this.logger.error('[Enrolment] EnrolmentResource::assignAgreementType error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Admin
  // ---

  public createAdmin(admin: Admin): Observable<Admin> {
    admin.status = AdminStatusType.ENABLED;
    return this.apiResource.post<Admin>('admins', admin)
      .pipe(
        map((response: ApiHttpResponse<Admin>) => response.result),
        tap((newAdmin: Admin) => this.logger.info('NEW_ADMIN', newAdmin)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::createAdmin error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdjudicators(): Observable<Admin[]> {
    return this.apiResource.get<Admin[]>('admins')
      .pipe(
        map((response: ApiHttpResponse<Admin[]>) => response.result),
        tap((admins: Admin[]) => this.logger.info('ADMINS', admins)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getAdjudicators error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdminUsers(): Observable<AdminUser[]> {
    return this.apiResource.get<AdminUser[]>('admins/adminusers')
      .pipe(
        map((response: ApiHttpResponse<AdminUser[]>) => response.result),
        tap((admins: AdminUser[]) => this.logger.info('ADMIN USERS', admins)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getAdjudicators error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdjudicatorByUserId(userId: string): Observable<Admin> {
    return this.apiResource.get<Admin>(`admins/${userId}`)
      .pipe(
        map((response: ApiHttpResponse<Admin>) => response.result),
        tap((admin: Admin) => this.logger.info('ADMIN', admin)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }
          this.logger.error('[Adjudication] AdjudicationResource::getAdjudicatorByUserId error has occurred: ', error);
          throw error;
        })
      );
  }

  public enableAdmin(adminId: number): Observable<Admin> {
    return this.apiResource.put<Admin>(`admins/${adminId}/enable`)
      .pipe(
        map((response: ApiHttpResponse<Admin>) => response.result),
        tap((admin: Admin) => this.logger.info('ADMIN', admin)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::enableAdmin error has occurred: ', error);
          throw error;
        })
      );
  }

  public disableAdmin(adminId: number): Observable<Admin> {
    return this.apiResource.put<Admin>(`admins/${adminId}/disable`)
      .pipe(
        map((response: ApiHttpResponse<Admin>) => response.result),
        tap((admin: Admin) => this.logger.info('ADMIN', admin)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::disableAdmin error has occurred: ', error);
          throw error;
        })
      );
  }

  public getMetabaseToken(): Observable<string> {
    return this.apiResource.get<string>('admins/embedded-metabase-url')
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap((token: string) => this.logger.info('METABASE_TOKEN', token)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getMetabaseToken error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrolmentReference(enrolleeId: number): Observable<EnrolmentStatusReference> {
    return this.apiResource.post(`enrollees/${enrolleeId}/status-reference`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentStatusReference>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::createEnrolmentReference error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Notifications
  // ---

  public getNotificationsByEnrollee(enrolleeId: number): Observable<EnrolleeNote[]> {
    return this.apiResource.get(`enrollees/${enrolleeId}/notifications`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote[]>) => response.result),
        tap((adjudicatorNotes: EnrolleeNote[]) => this.logger.info('ENROLLEE_NOTIFICATIONS', adjudicatorNotes)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Notification could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getNotificationsByEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrolleeNotification(enrolleeId: number, enrolleeNoteId: number, assigneeId: number): Observable<EnrolleeNotification> {
    const payload = { data: assigneeId };
    return this.apiResource.post(`enrollees/${enrolleeId}/adjudicator-notes/${enrolleeNoteId}/notification`, payload)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNotification>) => response.result),
        tap((notification: EnrolleeNotification) => {
          this.toastService.openErrorToast('Enrolment Notification has been saved');
          this.logger.info('NEW_ENROLMENT_ESCALTION', notification);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee Notification has been saved');
          this.logger.error('[Adjudication] AdjudicationResource::createEnrolleeNotification error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteEnrolleeNotification(enrolleeId: number, enrolleeNoteId: number) {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${enrolleeId}/adjudicator-notes/${enrolleeNoteId}/notification`)
      .pipe(
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee Notification could not be deleted');
          this.logger.error('[Adjudication] AdjudicationResource::deleteEnrolleeNotification error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteEnrolleeNotifications(enrolleeId: number) {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${enrolleeId}/notifications`)
      .pipe(
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee Notifications could not be deleted');
          this.logger.error('[Adjudication] AdjudicationResource::deleteEnrolleeNotifications error has occurred: ', error);
          throw error;
        })
      );
  }

  public getNotificationsBySite(siteId: number): Observable<SiteRegistrationNote[]> {
    return this.apiResource.get(`sites/${siteId}/notifications`)
      .pipe(
        map((response: ApiHttpResponse<SiteRegistrationNote[]>) => response.result),
        tap((siteRegistrationNotes: SiteRegistrationNote[]) => this.logger.info('SITE_NOTIFICATIONS', siteRegistrationNotes)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Notifications could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getNotificationsBySite error has occurred: ', error);
          throw error;
        })
      );
  }

  public createSiteNotification(siteId: number, siteRegistrationNoteId: number, assigneeId: number): Observable<SiteNotification> {
    const payload = { data: assigneeId };
    return this.apiResource.post(`sites/${siteId}/site-registration-notes/${siteRegistrationNoteId}/notification`, payload)
      .pipe(
        map((response: ApiHttpResponse<SiteNotification>) => response.result),
        tap((notification: SiteNotification) => {
          this.toastService.openErrorToast('Site Notification has been saved');
          this.logger.info('NEW_ENROLMENT_ESCALTION', notification);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Notification has been saved');
          this.logger.error('[Adjudication] AdjudicationResource::createSiteNotification error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSiteNotification(siteId: number, siteRegistrationNoteId: number) {
    return this.apiResource.delete(`sites/${siteId}/site-registration-notes/${siteRegistrationNoteId}/notification`)
      .pipe(
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Notification could not be deleted');
          this.logger.error('[Adjudication] AdjudicationResource::deleteSiteNotification error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSiteNotifications(siteId: number) {
    return this.apiResource.delete(`sites/${siteId}/notifications`)
      .pipe(
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::deleteSiteNotifications error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeEmails(bulkEmailType: BulkEmailType): Observable<string[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ bulkEmailType });
    return this.apiResource.get<string[]>('enrollees/emails', params)
      .pipe(
        map((response: ApiHttpResponse<string[]>) => response.result),
        tap((enrollees: string[]) => this.logger.info('ENROLLEE_EMAILS', enrollees)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollees bulk emails could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeEmails error has occurred: ', error);
          throw error;
        })
      );
  }

  public updatePaperEnrolleeDateOfBirth(enrolleeId: number, dateOfBirth: moment.Moment): NoContent {
    const payload = { data: dateOfBirth };
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/date-of-birth`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollees date of birth could not be updated');
          this.logger.error('[Adjudication] AdjudicationResource::updatePaperEnrolleeDateOfBirth error has occurred: ', error);
          throw error;
        })
      );
  }


  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): HttpEnrollee {
    addressTypes.forEach((addressType: AddressType) => {
      if (!enrollee[addressType]) {
        enrollee[addressType] = new Address();
      }
    });

    if (!enrollee.certifications) {
      enrollee.certifications = [];
    }

    if (!enrollee.unlistedCertifications) {
      enrollee.unlistedCertifications = [];
    }

    if (!enrollee.oboSites) {
      enrollee.oboSites = [];
    }

    if (!enrollee.enrolleeCareSettings) {
      enrollee.enrolleeCareSettings = [];
    }

    if (!enrollee.enrolleeRemoteUsers) {
      enrollee.enrolleeRemoteUsers = [];
    }

    if (!enrollee.remoteAccessSites) {
      enrollee.remoteAccessSites = [];
    }

    if (!enrollee.enrolleeHealthAuthorities) {
      enrollee.enrolleeHealthAuthorities = [];
    }

    if (!enrollee.remoteAccessLocations) {
      enrollee.remoteAccessLocations = [];
    }

    if (!enrollee.selfDeclarations) {
      enrollee.selfDeclarations = [];
    }

    if (!enrollee.selfDeclarationDocuments) {
      enrollee.selfDeclarationDocuments = [];
    }

    return enrollee;
  }

  private enrolleeSubmissionAdapterResponse(): (enrolleeSubmission: HttpEnrolleeSubmission) => HttpEnrolleeSubmission {
    return ({ id, enrolleeId, profileSnapshot, agreementType, createdDate }: HttpEnrolleeSubmission) => {
      // Compensate for updates to the current enrolment model
      // that don't match enrolment versioning
      this.enrolleeSubmissionSnapshotAdapter(profileSnapshot);

      return {
        id,
        enrolleeId,
        profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
        agreementType,
        createdDate
      };
    };
  }

  private enrolleeSubmissionSnapshotAdapter(profileSnapshot: HttpEnrollee): void {
    const mapping = {
      voicePhone: 'phone',
      voiceExtension: 'phoneExtension',
      contactEmail: 'email',
      contactPhone: 'smsPhone'
    };
    ObjectUtils.keyMapping(profileSnapshot, mapping);

    // Key index aligns with SelfDeclarationTypeEnum
    const selfDeclarations = {
      hasConviction: 'Has Conviction',
      hasRegistrationSuspended: 'Has Registration Suspended',
      hasPharmaNetSuspended: 'Has PharmaNet Suspended',
      hasDisciplinaryAction: 'Has Disciplinary Action',
    };
    const keys = Object.keys(selfDeclarations);

    if (keys.every((key: string) => profileSnapshot.hasOwnProperty(key))) {
      profileSnapshot.selfDeclarations = [];
      keys.forEach((key: string, index: number) => {
        if (profileSnapshot[key]) {
          profileSnapshot.selfDeclarations.push({
            selfDeclarationDetails: profileSnapshot[`${key}Details`],
            selfDeclarationTypeCode: index + 1,
            answered: true,
          });
        }

        delete profileSnapshot[key];
        delete profileSnapshot[`${key}Details`];
      });
    }

    // Update enrolleeOrganizationTypes to enrolleeCareSettings
    if (profileSnapshot.hasOwnProperty('enrolleeOrganizationTypes')) {
      profileSnapshot.enrolleeCareSettings = [];
      const enrolleeOrganizationTypes = profileSnapshot[`enrolleeOrganizationTypes`];
      enrolleeOrganizationTypes.map(({ id, organizationTypeCode }) => {
        profileSnapshot.enrolleeCareSettings.push({
          id,
          careSettingCode: organizationTypeCode
        });
      });
      delete profileSnapshot[`enrolleeOrganizationTypes`];
    }

    // set answered property
    if (profileSnapshot.selfDeclarations) {
      profileSnapshot.selfDeclarations.forEach(sd => {
        sd.answered = !!sd.id;
      });
    }

    // create an ordered list of self declaratoin types
    let orderedSelfDeclarationType = [
      Number(SelfDeclarationTypeEnum.HAS_CONVICTION),
      Number(SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED),
      Number(SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED),
      Number(SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION)];

    // add unanswered self declaration questions and order the questions
    let orderedSelfDeclarations = [];
    for (var i = 0; i < 4; i++) {
      if (!isNaN(orderedSelfDeclarationType[i]) && !profileSnapshot.selfDeclarations.find(s => s.selfDeclarationTypeCode === orderedSelfDeclarationType[i])) {
        let unansweredSelfDeclaration = new SelfDeclaration(orderedSelfDeclarationType[i], null, null, null, false, null, null);
        orderedSelfDeclarations.push(unansweredSelfDeclaration);
      } else {
        orderedSelfDeclarations.push(profileSnapshot.selfDeclarations.find(s => s.selfDeclarationTypeCode === orderedSelfDeclarationType[i]));
      }
    }
    profileSnapshot.selfDeclarations = orderedSelfDeclarations;
  }

  /******************************
   * Organization Page resource
   ******************************/

  public getOrganizations(searchText: string): Observable<OrganizationAdminView[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ searchText });
    return this.apiResource.get<OrganizationAdminView[]>('organizations/admin-view', params)
      .pipe(
        map((response: ApiHttpResponse<OrganizationAdminView[]>) => response.result),
        tap((organizations: OrganizationAdminView[]) => this.logger.info('ORGANIZATION_ADMIN_VIEW', organizations)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization admin view could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getOrganizations error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationById(id: number): Observable<OrganizationAdminView> {
    return this.apiResource.get<OrganizationAdminView>(`organizations/admin-view/${id}`)
      .pipe(
        map((response: ApiHttpResponse<OrganizationAdminView>) => response.result),
        tap((organization: OrganizationAdminView) => this.logger.info('ORGANIZATION_ADMIN_VIEW', organization)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization admin view could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getOrganizationById error has occurred: ', error);
          throw error;
        })
      );
  }
}
