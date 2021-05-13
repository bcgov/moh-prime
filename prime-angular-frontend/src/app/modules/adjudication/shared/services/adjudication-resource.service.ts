import { EnrolleeNavigation } from './../../../../shared/models/enrollee-navigation-model';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { Address, AddressType, addressTypes } from '@shared/models/address.model';
import { EnrolleeAgreement } from '@shared/models/agreement.model';
import { HttpEnrolleeSubmission } from '@shared/models/enrollee-submission.model';
import { HttpEnrollee, EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolmentStatusReference } from '@shared/models/enrolment-status-reference.model';
import { EnrolmentCard } from '@shared/models/enrolment-card.model';
import { Admin } from '@auth/shared/models/admin.model';

import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { EnrolleeNotification } from '../models/enrollee-notification.model';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { SiteNotification } from '../models/site-notification.model';
import { BulkEmailType } from '@shared/enums/bulk-email-type';


@Injectable({
  providedIn: 'root'
})
export class AdjudicationResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public getEnrollees(textSearch?: string, statusCode?: number): Observable<EnrolleeListViewModel[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ textSearch, statusCode });
    return this.apiResource.get<EnrolleeListViewModel[]>('enrollees', params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeListViewModel[]>) => response.result),
        tap((enrollees: EnrolleeListViewModel[]) => this.logger.info('ENROLLEES', enrollees)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolments could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrollees error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeById(enrolleeId: number, statusCode?: number): Observable<HttpEnrollee> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode });
    return this.apiResource.get<HttpEnrollee>(`enrollees/${enrolleeId}`, params)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeById error has occurred: ', error);
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

  public submissionAction(enrolleeId: number, action: SubmissionAction): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>(`enrollees/${enrolleeId}/submission/${action}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => {
          this.toastService.openErrorToast('Enrolment status has been updated');
          this.logger.info('UPDATED_ENROLLEE', enrollee);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be updated');
          this.logger.error('[Adjudication] AdjudicationResource::submissionAction error has occurred: ', error);
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

  // ---
  // Agreements
  // ---

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
          this.toastService.openErrorToast('Site Notifications could not be deleted');
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

    if (!enrollee.jobs) {
      enrollee.jobs = [];
    }

    if (!enrollee.enrolleeCareSettings) {
      enrollee.enrolleeCareSettings = [];
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
      hasDisciplinaryAction: 'Has Disciplinary Action',
      hasPharmaNetSuspended: 'Has PharmaNet Suspended'
    };
    const keys = Object.keys(selfDeclarations);

    if (keys.every((key: string) => profileSnapshot.hasOwnProperty(key))) {
      profileSnapshot.selfDeclarations = [];
      keys.forEach((key: string, index: number) => {
        if (profileSnapshot[key]) {
          profileSnapshot.selfDeclarations.push({
            selfDeclarationDetails: profileSnapshot[`${key}Details`],
            selfDeclarationTypeCode: index + 1
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
  }
}
