import { Injectable } from '@angular/core';

import { forkJoin, Observable, of } from 'rxjs';
import { catchError, map, tap, exhaustMap } from 'rxjs/operators';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { EnrolleeStatusAction } from '@shared/enums/enrollee-status-action.enum';
import { Address, AddressType, addressTypes } from '@shared/models/address.model';
import { EnrolleeAgreement } from '@shared/models/agreement.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { EnrolmentSubmission, HttpEnrolleeSubmission } from '@shared/models/enrollee-submission.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';

import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';

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

  public enrollee(userId: string): Observable<Enrolment> {
    return this.apiResource.get<HttpEnrollee>(`enrollees/${userId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee) => this.logger.info('ENROLLEE', enrollee)),
        exhaustMap((enrollee) =>
          forkJoin([
            this.getAccessAgreementNotesByEnrolleeId(enrollee.id),
            this.getCareSettingsByEnrolleeId(enrollee.id),
            this.getCertificationsByEnrolleeId(enrollee.id),
            this.getRemoteUsersByEnrolleeId(enrollee.id),
            this.getOboSitesByEnrolleeId(enrollee.id),
            this.getRemoteAccessLocationsByEnrolleeId(enrollee.id),
            this.getRemoteAccessSitesByEnrolleeId(enrollee.id),
            this.getSelfDeclarationsByEnrolleeId(enrollee.id),
            this.getSelfDeclarationsDocumentsById(enrollee.id),
          ]).pipe(
            map(([accessAgreementNote, enrolleeCareSettings, certifications, enrolleeRemoteUsers, oboSites, remoteAccessLocations, remoteAccessSites, selfDeclarations, selfDeclarationDocuments]: [EnrolleeNote, CareSetting, CollegeCertification[], EnrolleeRemoteUser[], OboSite[], RemoteAccessLocation[], RemoteAccessSite[], SelfDeclaration[], SelfDeclarationDocument[]]) => {
              return { ...enrollee, accessAgreementNote, certifications, ...enrolleeCareSettings, enrolleeRemoteUsers, oboSites, remoteAccessLocations, remoteAccessSites, selfDeclarations, selfDeclarationDocuments }
            }),
          )),
        map((enrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }
          this.logger.error('[Enrolment] EnrolmentResource::enrollee error has occurred: ', error);
          throw error;
        })
      );
    // return forkJoin([
    //   this.apiResource.get<HttpEnrollee>(`enrollees/${userId}`)
    //     .pipe(
    //       map((response: ApiHttpResponse<HttpEnrollee>) => response.result)),
    //   this.getAccessAgreementNotesByEnrolleeId(userId),
    //   this.getCareSettingsByEnrolleeId(userId),
    //   this.getCertificationsByEnrolleeId(userId),
    //   this.getRemoteUsersByEnrolleeId(userId),
    //   this.getOboSitesByEnrolleeId(userId),
    //   this.getRemoteAccessLocationsByEnrolleeId(userId),
    //   this.getRemoteAccessSitesByEnrolleeId(userId),
    //   this.getSelfDeclarationsByEnrolleeId(userId),
    //   this.getSelfDeclarationsDocumentsById(userId),
    // ]).pipe(
    //   map(([enrollee, accessAgreementNote, enrolleeCareSettings, certifications, enrolleeRemoteUsers, oboSites, remoteAccessLocations, remoteAccessSites, selfDeclarations, selfDeclarationDocuments]: [HttpEnrollee, EnrolleeNote, CareSetting, CollegeCertification[], EnrolleeRemoteUser[], OboSite[], RemoteAccessLocation[], RemoteAccessSite[], SelfDeclaration[], SelfDeclarationDocument[]]) => {
    //     return { ...enrollee, accessAgreementNote, certifications, ...enrolleeCareSettings, enrolleeRemoteUsers, oboSites, remoteAccessLocations, remoteAccessSites, selfDeclarations, selfDeclarationDocuments }
    //   }),
    //   tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
    //   map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
    //   catchError((error: any) => {
    //     this.toastService.openErrorToast('Enrolment could not be retrieved');
    //     this.logger.error('[Enrolment] EnrolmentResource::enrollee error has occurred:', error);
    //     throw error;
    //   })
    // );
  }

  public getAccessAgreementNotesByEnrolleeId(enrolleeId: number): Observable<EnrolleeNote> {
    return this.apiResource.get<EnrolleeNote>(`enrollees/${enrolleeId}/access-agreement-notes`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeNote>) => response.result),
        tap((notes: EnrolleeNote) => this.logger.info('ENROLLEE AGREEMENT NOTES', notes)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee access agreement notes could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeAccessAgreementNotes error has occurred: ', error);
          throw error;
        })
      );
  }

  public getCareSettingsByEnrolleeId(enrolleeId: number): Observable<CareSetting> {
    return this.apiResource.get<CareSetting>(`enrollees/${enrolleeId}/care-settings`)
      .pipe(
        map((response: ApiHttpResponse<CareSetting>) => response.result),
        tap((careSettings: CareSetting) => this.logger.info('ENROLLEE CARE SETTINGS', careSettings)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeCareSettings error has occurred: ', error);
          throw error;
        })
      );
  }

  public getCertificationsByEnrolleeId(enrolleeId: number): Observable<CollegeCertification[]> {
    return this.apiResource.get<CollegeCertification[]>(`enrollees/${enrolleeId}/certifications`)
      .pipe(
        map((response: ApiHttpResponse<CollegeCertification[]>) => response.result),
        tap((certifications: CollegeCertification[]) => this.logger.info('ENROLLEE CERTIFICATIONS', certifications)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeCertifications error has occurred: ', error);
          throw error;
        })
      );
  }

  public getRemoteUsersByEnrolleeId(enrolleeId: number): Observable<EnrolleeRemoteUser[]> {
    return this.apiResource.get<EnrolleeRemoteUser[]>(`enrollees/${enrolleeId}/remote-users`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeRemoteUser[]>) => response.result),
        tap((remoteUsers: EnrolleeRemoteUser[]) => this.logger.info('ENROLLEE REMOTE USERS', remoteUsers)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeRemoteUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOboSitesByEnrolleeId(enrolleeId: number): Observable<OboSite[]> {
    return this.apiResource.get<OboSite[]>(`enrollees/${enrolleeId}/obo-sites`)
      .pipe(
        map((response: ApiHttpResponse<OboSite[]>) => response.result),
        tap((oboSites: OboSite[]) => this.logger.info('ENROLLEE OBO SITES', oboSites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeOboSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getRemoteAccessLocationsByEnrolleeId(enrolleeId: number): Observable<RemoteAccessLocation[]> {
    return this.apiResource.get<RemoteAccessLocation[]>(`enrollees/${enrolleeId}/remote-locations`)
      .pipe(
        map((response: ApiHttpResponse<RemoteAccessLocation[]>) => response.result),
        tap((remoteAccessLocations: RemoteAccessLocation[]) => this.logger.info('ENROLLEE REMOTE ACCESS LOCATIONS', remoteAccessLocations)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeRemoteAccessLocations error has occurred: ', error);
          throw error;
        })
      );
  }

  public getRemoteAccessSitesByEnrolleeId(enrolleeId: number): Observable<RemoteAccessSite[]> {
    return this.apiResource.get<RemoteAccessSite[]>(`enrollees/${enrolleeId}/remote-sites`)
      .pipe(
        map((response: ApiHttpResponse<RemoteAccessSite[]>) => response.result),
        tap((remoteAccessSites: RemoteAccessSite[]) => this.logger.info('REMOTE ACCESS SITES', remoteAccessSites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeRemoteAccessSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSelfDeclarationsByEnrolleeId(enrolleeId: number): Observable<SelfDeclaration[]> {
    return this.apiResource.get<SelfDeclaration[]>(`enrollees/${enrolleeId}/self-declarations`)
      .pipe(
        map((response: ApiHttpResponse<SelfDeclaration[]>) => response.result),
        tap((selfDeclarations: SelfDeclaration[]) => this.logger.info('ENROLLEE SELF DECLARATIONS', selfDeclarations)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeSelfDeclarations error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSelfDeclarationsDocumentsById(enrolleeId: number): Observable<SelfDeclarationDocument[]> {
    return this.apiResource.get<SelfDeclarationDocument[]>(`enrollees/${enrolleeId}/self-declarations/documents`)
      .pipe(
        map((response: ApiHttpResponse<SelfDeclarationDocument[]>) => response.result),
        tap((SelfDeclarationsDocument: SelfDeclarationDocument[]) => this.logger.info('ENROLLEE SELF DECLARATIONS DOCUMENTS', SelfDeclarationsDocument)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee care settings could not be retrieved');
          this.logger.error('[Enrolment] AdjudicationResource::getEnrolleeSelfDeclarations error has occurred: ', error);
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

  public getCurrentStatus(enrolleeId: number): Observable<EnrolmentStatus> {
    return this.apiResource.get<EnrolmentStatus>(`enrollees/${enrolleeId}/current-status`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentStatus>) => response.result),
        tap((accessTerms: EnrolmentStatus) => this.logger.info('ENROLLEE_AGREEMENTS', accessTerms)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee current status could not be found.');
          this.logger.error('[Enrolment] EnrolmentResource::getCurrentStatus error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Provisioner Access
  // ---

  public sendProvisionerAccessLink(
    emails: string = null, enrolleeId: number, careSettingCode: number
  ): Observable<EnrolmentCertificateAccessToken> {
    const payload = { data: emails };
    return this.apiResource
      .post<EnrolmentCertificateAccessToken>(`enrollees/${enrolleeId}/provisioner-access/send-link/${careSettingCode}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCertificateAccessToken>) => response.result),
        tap((token: EnrolmentCertificateAccessToken) => this.logger.info('ACCESS_TOKEN', token)),
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

  public deleteEnrolleeAdjudicationDocument(enrolleeId: number, documentId: number) {
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
        phoneExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      careSettings: enrollee.enrolleeCareSettings,
      enrolleeRemoteUsers: enrollee.enrolleeRemoteUsers,
      remoteAccessSites: enrollee.remoteAccessSites,
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

    enrolment.certifications = this.removeIncompleteCollegeCertifications(enrolment.certifications);
    enrolment.careSettings = this.removeIncompleteCareSettings(enrolment.careSettings);

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

  private removeIncompleteCollegeCertifications(certifications: CollegeCertification[]) {
    return certifications.filter((certification: CollegeCertification) =>
      this.collegeCertificationIsIncomplete(certification)
    );
  }

  private collegeCertificationIsIncomplete(certification: CollegeCertification): boolean {
    const allowlist = ['practiceCode', 'practitionerId'];

    return Object.keys(certification)
      .every((key: string) =>
        (!allowlist.includes(key) && !certification[key]) ? certification[key] : true
      );
  }

  private removeIncompleteCareSettings(careSettings: CareSetting[]) {
    return careSettings.filter((careSetting: CareSetting) => careSetting.careSettingCode);
  }
}
