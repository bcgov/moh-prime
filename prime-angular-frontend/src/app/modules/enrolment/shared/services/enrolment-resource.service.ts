import { Injectable } from '@angular/core';

import { forkJoin, Observable, of } from 'rxjs';
import { catchError, map, tap, exhaustMap } from 'rxjs/operators';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { EnrolleeStatusAction } from '@shared/enums/enrollee-status-action.enum';
import { EnrolleeAgreement } from '@shared/models/agreement.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { EnrolmentSubmission, HttpEnrolleeSubmission } from '@shared/models/enrollee-submission.model';
import { EnrolleeAbsence } from '@shared/models/enrollee-absence.model';
import { EnrolmentStatusAdmin } from '@shared/models/enrolment-status-admin.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';

import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { SelfDeclarationVersion } from '@shared/models/self-declaration-version.model';
import { EmailsForCareSetting } from '@shared/models/email-for-care-setting.model';
import { EnrolleeDeviceProvider } from '@shared/models/enrollee-device-provider.model';
import { DeviceProviderSite } from "@shared/models/device-provider-site.model";
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public enrollee(username: string): Observable<Enrolment> {
    const selfDeclarationDocumentsParams = this.apiResourceUtilsService.makeHttpParams({ includeHidden: false });
    return this.apiResource.get<HttpEnrollee>(`enrollees/${username}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee) => this.logger.info('ENROLLEE', enrollee)),
        exhaustMap((enrollee) =>
          forkJoin({
            enrolleeCareSettings: this.apiResource.get<CareSetting>(`enrollees/${enrollee.id}/care-settings`)
              .pipe(map((response: ApiHttpResponse<CareSetting>) => response.result)),
            certifications: this.apiResource.get<CollegeCertification[]>(`enrollees/${enrollee.id}/certifications`)
              .pipe(map((response: ApiHttpResponse<CollegeCertification[]>) => response.result)),
            unlistedCertifications: this.apiResource.get<UnlistedCertification[]>(`enrollees/${enrollee.id}/unlisted-certifications`)
              .pipe(map((response: ApiHttpResponse<UnlistedCertification[]>) => response.result)),
            enrolleeDeviceProviders: this.apiResource.get<EnrolleeDeviceProvider[]>(`enrollees/${enrollee.id}/device-providers`)
              .pipe(map((response: ApiHttpResponse<EnrolleeDeviceProvider[]>) => response.result)),
            enrolleeRemoteUsers: this.apiResource.get<EnrolleeRemoteUser[]>(`enrollees/${enrollee.id}/remote-users`)
              .pipe(map((response: ApiHttpResponse<EnrolleeRemoteUser[]>) => response.result)),
            oboSites: this.apiResource.get<OboSite[]>(`enrollees/${enrollee.id}/obo-sites`)
              .pipe(map((response: ApiHttpResponse<OboSite[]>) => response.result)),
            remoteAccessLocations: this.apiResource.get<RemoteAccessLocation[]>(`enrollees/${enrollee.id}/remote-locations`)
              .pipe(map((response: ApiHttpResponse<RemoteAccessLocation[]>) => response.result)),
            remoteAccessSites: this.apiResource.get<RemoteAccessSite[]>(`enrollees/${enrollee.id}/remote-sites`)
              .pipe(map((response: ApiHttpResponse<RemoteAccessSite[]>) => response.result)),
            selfDeclarations: this.apiResource.get<SelfDeclaration[]>(`enrollees/${enrollee.id}/self-declarations`)
              .pipe(map((response: ApiHttpResponse<SelfDeclaration[]>) => response.result)),
            selfDeclarationDocuments: this.apiResource.get<SelfDeclarationDocument[]>(`enrollees/${enrollee.id}/self-declarations/documents`, selfDeclarationDocumentsParams)
              .pipe(map((response: ApiHttpResponse<SelfDeclarationDocument[]>) => response.result))
          })
            .pipe(
              map(({ enrolleeCareSettings, ...remainder }) => {
                return { ...enrollee, ...enrolleeCareSettings, ...remainder };
              }),
            ),
        ),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }
          this.logger.error('[Enrolment] EnrolmentResource::enrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrollee(payload: { enrollee: Enrollee, identificationDocumentGuid?: string }): Observable<Enrolment> {
    return this.apiResource.post<HttpEnrollee>('enrollees', payload)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be created.');
          this.logger.error('[Enrolment] EnrolmentResource::createEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public checkForMatchingPaperSubmission(dateOfBirth: string): Observable<boolean> {
    const params = this.apiResourceUtilsService.makeHttpParams({ dateOfBirth });
    return this.apiResource.head<boolean>('enrollees/paper-submissions', params)
      .pipe(
        map(() => true),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(false);
          }
          this.logger.error('[Enrolment] EnrolmentResource::checkForMatchingPaperSubmission error has occurred:  ', error);
          throw error;
        })
      );
  }

  public createOrUpdateLinkedGpid(enrolleeId: number, paperEnrolleeGpid: string): Observable<NoContent> {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/linked-gpid`, { data: paperEnrolleeGpid })
      .pipe(
        map((response: ApiHttpResponse<NoContent>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::createOrUpdateLinkedGpid error has occurred: ', error);
          throw error;
        })
      );
  }

  public getLinkedGpid(enrolleeId: number): Observable<string | null> {
    return this.apiResource.get<string>(`enrollees/${enrolleeId}/linked-gpid`)
      .pipe(
        map((response: ApiHttpResponse<string | null>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getLinkedGpid error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEnrollee(enrolment: Enrolment, beenThroughTheWizard: boolean = false): NoContent {
    const { id } = enrolment;
    const params = this.apiResourceUtilsService.makeHttpParams({ beenThroughTheWizard });
    return this.apiResource.put<NoContent>(`enrollees/${id}`, this.enrolmentAdapterRequest(enrolment), params)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be updated.');
          this.logger.error('[Enrolment] EnrolmentResource::updateEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public submitApplication(enrolment: Enrolment): Observable<HttpEnrollee> {
    const { id } = enrolment;
    return this.apiResource.post<HttpEnrollee>(`enrollees/${id}/submissions`, this.enrolmentAdapterRequest(enrolment))
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Application submission could not be completed.');
          this.logger.error('[Enrolment] EnrolmentResource::submitApplication error has occurred: ', error);
          throw error;
        })
      );
  }

  public enrolleeStatusAction(id: number, action: EnrolleeStatusAction, documentGuid: string = null): Observable<HttpEnrollee> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid });
    return this.apiResource.post<HttpEnrollee>(`enrollees/${id}/status-actions/${action}`, {}, params)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Action could not be completed.');
          this.logger.error('[Enrolment] EnrolmentResource::enrolleeStatusAction error has occurred: ', error);
          throw error;
        })
      );
  }

  public returnToEditing(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>(`enrollees/${enrolleeId}/status-actions/return-to-editing`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => {
          this.toastService.openErrorToast('Enrolment is now editable');
          this.logger.info('UPDATED_ENROLLEE', enrollee);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be updated');
          this.logger.error('[Enrolment] EnrolmentResource::returnToEditing error has occurred: ', error);
          throw error;
        })
      );
  }

  public getCurrentStatus(enrolleeId: number): Observable<EnrolmentStatusAdmin> {
    return this.apiResource.get<EnrolmentStatusAdmin>(`enrollees/${enrolleeId}/current-status`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentStatusAdmin>) => response.result),
        tap((accessTerms: EnrolmentStatusAdmin) => this.logger.info('ENROLLEE_AGREEMENTS', accessTerms)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee current status could not be found.');
          this.logger.error('[Enrolment] EnrolmentResource::getCurrentStatus error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSelfDeclarationVersion(targetDate: string, isDeviceProvider: boolean): Observable<SelfDeclarationVersion[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ targetDate, isDeviceProvider });
    return this.apiResource.get<SelfDeclarationVersion[]>(`lookups/self-declaration-question`, params)
      .pipe(
        map((response: ApiHttpResponse<SelfDeclarationVersion[]>) => response.result),
        tap((selfDeclarationVersions: SelfDeclarationVersion[]) => this.logger.info('SELF_DECLARATION_VERSION', selfDeclarationVersions)),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getSelfDeclarationVersion error has occurred: ', error);
          // release to allow the application to render
          return of(null);
        })
      )
  }

  // ---
  // Provisioner Access
  // ---

  public sendProvisionerAccessLink(
    emailPairs: EmailsForCareSetting[] = [], enrolleeId: number
  ): Observable<EnrolmentCertificateAccessToken[]> {
    return this.apiResource
      .post<EnrolmentCertificateAccessToken[]>(`enrollees/${enrolleeId}/provisioner-access/send-link`, emailPairs)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCertificateAccessToken[]>) => response.result),
        tap((token: EnrolmentCertificateAccessToken[]) => this.logger.info('ACCESS_TOKEN', token)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email could not be sent');
          this.logger.error('[Enrolment] EnrolmentResource::sendProvisionerAccessLink error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Agreements
  // ---

  public getAcceptedAccessTerms(enrolleeId: number): Observable<EnrolleeAgreement[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ accepted: true });
    return this.apiResource.get<EnrolleeAgreement[]>(`enrollees/${enrolleeId}/agreements`, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement[]>) => response.result),
        tap((accessTerms: EnrolleeAgreement[]) => this.logger.info('ENROLLEE_AGREEMENTS', accessTerms)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee agreements could not be found.');
          this.logger.error('[Enrolment] EnrolmentResource::getAcceptedAccessTerms error has occurred: ', error);
          throw error;
        })
      );
  }

  public getLatestAccessTerm(enrolleeId: number, accepted: boolean): Observable<EnrolleeAgreement> {
    const params = this.apiResourceUtilsService.makeHttpParams({ onlyLatest: true, accepted, includeText: true });
    return this.apiResource.get<EnrolleeAgreement[]>(`enrollees/${enrolleeId}/agreements`, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement[]>) => response.result.pop()),
        tap((accessTerm: EnrolleeAgreement) => this.logger.info('LATEST_ENROLLEE_AGREEMENT', accessTerm)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee agreement could not be found.');
          this.logger.error('[Enrolment] EnrolmentResource::getLatestAccessTerm error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAccessTerm(enrolleeId: number, agreementId: number): Observable<EnrolleeAgreement> {
    return this.apiResource.get<EnrolleeAgreement>(`enrollees/${enrolleeId}/agreements/${agreementId}`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement>) => response.result),
        tap((accessTerm: EnrolleeAgreement) => this.logger.info('ENROLLEE_AGREEMENT', accessTerm)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee agreement could not be found.');
          this.logger.error('[Enrolment] EnrolmentResource::getAccessTerm error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAccessTermSignable(enrolleeId: number, accessTermsId: number): Observable<string> {
    return this.apiResource.get<string>(`enrollees/${enrolleeId}/agreements/${accessTermsId}/signable`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result)
      );
  }

  public getEnrolmentSubmissionForAccessTerm(enrolleeId: number, agreementId: number): Observable<EnrolmentSubmission> {
    return this.apiResource.get<HttpEnrolleeSubmission>(`enrollees/${enrolleeId}/agreements/${agreementId}/submission`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeSubmission>) => response.result),
        tap((enrolleeSubmission: HttpEnrolleeSubmission) => this.logger.info('ENROLMENT_SUBMISSION', enrolleeSubmission)),
        map(this.enrolleeSubmissionAdapterResponse()),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment profile could not be found.');
          this.logger.error('[Enrolment] EnrolmentResource::getEnrolmentSubmissionForAccessTerm error has occurred: ', error);
          throw error;
        })
      );
  }

  public getIsOboToRuAgreementTypeChange(enrolleeId: number): Observable<boolean> {
    return this.apiResource.get<boolean>(`enrollees/${enrolleeId}/agreements/current/obo-to-ru`)
      .pipe(
        map((response: ApiHttpResponse<boolean>) => response.result),
        tap((isChange: boolean) => this.logger.info('OBO_TO_RU', isChange)),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getIsOboToRuAgreementTypeChange error has occurred: ', error);
          throw error;
        })
      );
  }

  public getCurrentAgreementGroupForAnEnrollee(enrolleeId: number): Observable<AgreementTypeGroup> {
    return this.apiResource.get<AgreementTypeGroup>(`enrollees/${enrolleeId}/agreements/current/agreement-group`)
      .pipe(
        map((response: ApiHttpResponse<AgreementTypeGroup>) => response.result),
        tap((group: AgreementTypeGroup) => this.logger.info('AGREEMENT_GROUP', group)),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getCurrentAgreementGroupForAnEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public getQrCode(enrolleeId: number): Observable<string> {
    return this.apiResource.get<string>(`enrollees/${enrolleeId}/qrCode`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result)
      );
  }

  // ---
  // Self Declaration Documents
  // ---

  public getDownloadTokenSelfDeclarationDocument(enrolleeId: number, selfDeclarationDocumentId: number): Observable<string> {
    return this.apiResource
      .get<string>(`enrollees/${enrolleeId}/self-declaration-document/${selfDeclarationDocumentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap((document: string) => this.logger.info('DOCUMENT', document)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Document could not be downloaded.');
          this.logger.error('[Enrolment] EnrolmentResource::getDownloadTokenSelfDeclarationDocument error has occurred: ', error);
          throw error;
        })
      );
  }


  public getDownloadTokenIdentificationDocument(enrolleeId: number, identificationDocumentId: number): Observable<string> {
    return this.apiResource
      .get<string>(`enrollees/${enrolleeId}/identification-document/${identificationDocumentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap((document: string) => this.logger.info('DOCUMENT', document)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Document could not be downloaded.');
          this.logger.error('[Enrolment] EnrolmentResource::getDownloadTokenIdentificationDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrolleeAdjudicationDocument(enrolleeId: number, documentGuid: string): Observable<EnrolleeAdjudicationDocument> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid });
    return this
      .apiResource.post<EnrolleeAdjudicationDocument>(`enrollees/${enrolleeId}/adjudication-documents`, { enrolleeId }, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAdjudicationDocument>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::createEnrolleeAdjudicationDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeAdjudicationDocuments(enrolleeId: number): Observable<EnrolleeAdjudicationDocument[]> {
    return this.apiResource.get<EnrolleeAdjudicationDocument[]>(`enrollees/${enrolleeId}/adjudication-documents`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAdjudicationDocument[]>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getEnrolleeAdjudicationDocuments error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeAdjudicationDocumentDownloadToken(enrolleeId: number, documentId: number): Observable<string> {
    return this.apiResource.get<string>(`enrollees/${enrolleeId}/adjudication-documents/${documentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getEnrolleeAdjudicationDocumentDownloadToken error has occurred: ',
            error);
          throw error;
        })
      );
  }

  public deleteEnrolleeAdjudicationDocument(enrolleeId: number, documentId: number): Observable<EnrolleeAdjudicationDocument> {
    return this.apiResource.delete<EnrolleeAdjudicationDocument>(
      `enrollees/${enrolleeId}/adjudication-documents/${documentId}`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAdjudicationDocument>) => response.result),
        map((document: EnrolleeAdjudicationDocument) => document),
        tap((document: EnrolleeAdjudicationDocument) => {
          this.toastService.openSuccessToast('Document has been deleted');
          this.logger.info('DELETED_DOCUMENT', document);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Document could not be deleted');
          this.logger.error('[Adjudication] EnrolmentResource::deleteEnrolleeAdjudicationDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Enrollee Absesnces
  // ---

  public createEnrolleeAbsence(enrolleeId: number, startTimestamp: string, endTimestamp: string): Observable<NoContent> {
    const payload = { startTimestamp, endTimestamp };
    return this
      .apiResource.post<NoContent>(`enrollees/${enrolleeId}/absences`, payload)
      .pipe(
        map((response: ApiHttpResponse<NoContent>) => response.result),
        tap(() => this.toastService.openSuccessToast('Enrollee Absence has been created.')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee Absence could not be created.');
          this.logger.error('[Enrolment] EnrolmentResource::createEnrolleeAbsence error has occurred: ', error);
          throw error;
        })
      );
  }

  public endCurrentEnrolleeAbsence(enrolleeId: number): Observable<NoContent> {
    return this
      .apiResource.put<NoContent>(`enrollees/${enrolleeId}/absences/current/end`)
      .pipe(
        map((response: ApiHttpResponse<NoContent>) => response.result),
        tap(() => this.toastService.openSuccessToast('Enrollee Absence has been ended.')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee Absence could not be ended.');
          this.logger.error('[Enrolment] EnrolmentResource::endEnrolleeAbsence error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteFutureEnrolleeAbsence(enrolleeId: number, absenceId: number): Observable<NoContent> {
    return this
      .apiResource.delete<NoContent>(`enrollees/${enrolleeId}/absences/${absenceId}`)
      .pipe(
        map((response: ApiHttpResponse<NoContent>) => response.result),
        tap(() => this.toastService.openSuccessToast('Enrollee Absence has been removed.')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee Absence could not be removed.');
          this.logger.error('[Enrolment] EnrolmentResource::deleteFutureEnrolleeAbsence error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendEnrolleeAbsenceEmail(enrolleeId: number, email: string): Observable<NoContent> {
    const payload = { data: email };
    return this.apiResource
      .post<EnrolmentCertificateAccessToken>(`enrollees/${enrolleeId}/absences/email`, payload)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openErrorToast('Email has been sent')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email could not be sent');
          this.logger.error('[Enrolment] EnrolmentResource::sendEnrolleeAbsenceEmail error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeAbsences(enrolleeId: number): Observable<EnrolleeAbsence[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ includesPast: false });
    return this.apiResource
      .get<EnrolleeAbsence[]>(`enrollees/${enrolleeId}/absences`, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAbsence[]>) => response.result),
        tap((absences: EnrolleeAbsence[]) => this.logger.info('ENROLLEE_ABSENCES', absences)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee absences could not be retrieved.');
          this.logger.error('[Enrolment] EnrolmentResource::getEnrolleeAbsences error has occurred: ', error);
          throw error;
        })
      );
  }

  public getCurrentEnrolleeAbsence(enrolleeId: number): Observable<EnrolleeAbsence> {
    return this.apiResource
      .get<EnrolleeAbsence>(`enrollees/${enrolleeId}/absences/current`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAbsence>) => response.result),
        tap((absence: EnrolleeAbsence) => this.logger.info('CURRENT_ENROLLEE_ABSENCE', absence)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Current enrollee absence could not be retrieved.');
          this.logger.error('[Enrolment] EnrolmentResource::getCurrentEnrolleeAbsence error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAcceptedTermsOfAccessToken(enrolleeId: number): Observable<string> {
    return this.apiResource.get<string>(`enrollees/${enrolleeId}/agreement`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee terms of access could not be downloaded.');
          this.logger.error('[Enrolment] EnrolmentResource::getAcceptedTermsOfAccessToken error has occurred: ', error);
          throw error;
        }),
      );
  }

  public getDeviceProviderSite(deviceProviderId: number): Observable<DeviceProviderSite> {
    return this.apiResource.get<DeviceProviderSite>(`sites/device-provider-site/${deviceProviderId}`)
      .pipe(
        map((response: ApiHttpResponse<DeviceProviderSite>) => response.result),
        tap((site: DeviceProviderSite) => this.logger.info('DEVICE_PROVIDER_SITE', site)),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getDeviceProviderSite device provider site not found: ', error);
          throw error;
        })
      );
  }
  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleeSubmissionAdapterResponse(): (enrolleeSubmission: HttpEnrolleeSubmission) => EnrolmentSubmission {
    // Compensate for updates to the current enrolment model
    // that don't match enrolment versioning
    return ({ id, enrolleeId, profileSnapshot, agreementType, createdDate }: HttpEnrolleeSubmission) => {
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
      hasRegistrationSuspendedDeviceProvider: 'Has Registration Suspended for Device Provider',
      hasDisciplinaryAction: 'Has Disciplinary Action',
      hasPharmaNetSuspended: 'Has PharmaNet Suspended',
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
  }

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): Enrolment {
    addressTypes.forEach((addressType: AddressType) => {
      if (!enrollee[addressType]) {
        enrollee[addressType] = new Address();
      }
    });

    if (!enrollee.certifications) {
      enrollee.certifications = [];
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

    // Reorganize the shape of the enrollee into an enrolment
    return this.enrolmentAdapter(enrollee);
  }

  private enrolmentAdapter(enrollee: HttpEnrollee): Enrolment {
    const {
      userId,
      username,
      firstName,
      lastName,
      givenNames,
      preferredFirstName,
      preferredMiddleName,
      preferredLastName,
      dateOfBirth,
      gpid,
      hpdid,
      verifiedAddress,
      mailingAddress,
      physicalAddress,
      email,
      smsPhone,
      phone,
      phoneExtension,
      ...remainder
    } = enrollee;

    return {
      enrollee: {
        userId,
        username,
        firstName,
        lastName,
        givenNames,
        preferredFirstName,
        preferredMiddleName,
        preferredLastName,
        dateOfBirth,
        gpid,
        hpdid,
        verifiedAddress,
        mailingAddress,
        physicalAddress,
        email,
        smsPhone,
        phone,
        phoneExtension,
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      careSettings: enrollee.enrolleeCareSettings,
      enrolleeRemoteUsers: enrollee.enrolleeRemoteUsers,
      remoteAccessSites: enrollee.remoteAccessSites,
      selfDeclarationCompletedDate: enrollee.selfDeclarationCompletedDate,
      requireRedoSelfDeclaration: enrollee.requireRedoSelfDeclaration,
      ...remainder
    };
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): HttpEnrollee {
    addressTypes.forEach((addressType: AddressType) => {
      if (enrolment.enrollee[addressType]?.postal) {
        enrolment.enrollee[addressType].id = enrolment.enrollee[addressType].id ?? 0;
        enrolment.enrollee[addressType].postal = enrolment.enrollee[addressType].postal.toUpperCase();
      } else {
        enrolment.enrollee[addressType] = null;
      }
    });

    enrolment.selfDeclarations = this.removeUnansweredSelfDeclarationQuestions(enrolment.selfDeclarations);

    return this.enrolleeAdapter(enrolment);
  }

  private enrolleeAdapter(enrolment: Enrolment): HttpEnrollee {
    const {
      enrollee,
      ...remainder
    } = enrolment;

    return {
      ...enrollee,
      enrolleeCareSettings: enrolment.careSettings,
      enrolleeRemoteUsers: enrolment.enrolleeRemoteUsers,
      ...remainder
    };
  }

  // ---
  // Sanitization Helpers
  // ---

  private removeUnansweredSelfDeclarationQuestions(selfDeclarations: SelfDeclaration[]) {
    return selfDeclarations.filter((selfDeclaration: SelfDeclaration) => selfDeclaration.answered);
  }
}
