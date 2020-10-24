import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { Address } from '@shared/models/address.model';
import { EnrolleeAgreement } from '@shared/models/agreement.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { EnrolmentProfileVersion, HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private logger: LoggerService
  ) { }

  public enrollee(): Observable<Enrolment> {
    return this.apiResource.get<HttpEnrollee[]>('enrollees')
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee[]>) => response.result),
        tap((enrollees) => this.logger.info('ENROLLEES', enrollees[0])),
        map((enrollees) =>
          // Only a single enrollee will be provided
          (enrollees.length) ? this.enrolleeAdapterResponse(enrollees.pop()) : null
        )
      );
  }

  public createEnrollee(payload: { enrollee: Enrollee, identificationDocumentGuid?: string }): Observable<Enrolment> {
    return this.apiResource.post<HttpEnrollee>('enrollees', payload)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee))
      );
  }

  public updateEnrollee(enrolment: Enrolment, beenThroughTheWizard: boolean = false): NoContent {
    const { id } = enrolment;
    const params = this.apiResourceUtilsService.makeHttpParams({ beenThroughTheWizard });
    return this.apiResource.put<NoContent>(`enrollees/${id}`, this.enrolmentAdapterRequest(enrolment), params)
      .pipe(NoContentResponse);
  }

  public submitApplication(enrolment: Enrolment): Observable<HttpEnrollee> {
    const { id } = enrolment;
    return this.apiResource.post<HttpEnrollee>(`enrollees/${id}/submission`, this.enrolmentAdapterRequest(enrolment))
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
      );
  }

  public submissionAction(id: number, action: SubmissionAction): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>(`enrollees/${id}/submission/${action}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
      );
  }

  public createEnrolleeRemoteUsers(enrolleeId: number, sites: number[]): Observable<EnrolleeRemoteUser[]> {
    return this.apiResource
      .post<EnrolleeRemoteUser[]>(`enrollees/${enrolleeId}/enrollee-remote-users`, sites)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeRemoteUser[]>) => response.result)
      );
  }

  // ---
  // Provisioner Access
  // ---

  public enrolmentCertificateAccessTokens(): Observable<EnrolmentCertificateAccessToken[]> {
    return this.apiResource.get<EnrolmentCertificateAccessToken[]>('provisioner-access/token')
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCertificateAccessToken[]>) => response.result),
        tap((tokens: EnrolmentCertificateAccessToken[]) => this.logger.info('ACCESS_TOKENS', tokens))
      );
  }

  public sendProvisionerAccessLink(provisionerName: string, emails: string = null): Observable<EnrolmentCertificateAccessToken> {
    const payload = { data: emails };
    return this.apiResource.post<EnrolmentCertificateAccessToken>(`provisioner-access/send-link/${provisionerName}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCertificateAccessToken>) => response.result),
        tap((token: EnrolmentCertificateAccessToken) => this.logger.info('ACCESS_TOKEN', token))
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
        tap((accessTerms: EnrolleeAgreement[]) => this.logger.info('ACCESS_TERMS', accessTerms))
      );
  }

  public getLatestAccessTerm(enrolleeId: number, accepted: boolean): Observable<EnrolleeAgreement> {
    const params = this.apiResourceUtilsService.makeHttpParams({ onlyLatest: true, accepted, includeText: true });
    return this.apiResource.get<EnrolleeAgreement[]>(`enrollees/${enrolleeId}/agreements`, params)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement[]>) => response.result.pop()),
        tap((accessTerm: EnrolleeAgreement) => this.logger.info('ACCESS_TERM_LATEST', accessTerm))
      );
  }

  public getAccessTerm(enrolleeId: number, agreementId: number): Observable<EnrolleeAgreement> {
    return this.apiResource.get<EnrolleeAgreement>(`enrollees/${enrolleeId}/agreements/${agreementId}`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAgreement>) => response.result),
        tap((accessTerm: EnrolleeAgreement) => this.logger.info('ACCESS_TERM', accessTerm))
      );
  }

  public getEnrolmentProfileForAccessTerm(enrolleeId: number, agreementId: number): Observable<EnrolmentProfileVersion> {
    return this.apiResource.get<EnrolmentProfileVersion>(`enrollees/${enrolleeId}/agreements/${agreementId}/enrolment`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentProfileVersion>) => response.result),
        tap((enrolmentProfileVersion: EnrolmentProfileVersion) => this.logger.info('ENROLMENT_PROFILE_VERSION', enrolmentProfileVersion)),
        map(this.enrolleeVersionAdapterResponse.bind(this))
      );
  }

  // ---
  // Self Declaration Documents
  // ---

  public createSelfDeclarationDocument(enrolleeId: number, selfDeclarationStatusCode: number, sdd: SelfDeclarationDocument):
    Observable<SelfDeclarationDocument> {
    sdd.selfDeclarationTypeCode = selfDeclarationStatusCode;
    return this.apiResource
      .post<SelfDeclarationDocument>(`enrollees/${enrolleeId}/self-declaration-document`
        , sdd)
      .pipe(
        map((response: ApiHttpResponse<SelfDeclarationDocument>) => response.result),
        tap((selfDeclarationDocument: SelfDeclarationDocument) => this.logger.info('SELF_DECLARATION_DOCUMENT', selfDeclarationDocument)),
      );
  }

  public getDownloadTokenSelfDeclarationDocument(enrolleeId: number, selfDeclarationDocumentId: number): Observable<string> {
    return this.apiResource
      .get<string>(`enrollees/${enrolleeId}/self-declaration-document/${selfDeclarationDocumentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap((document: string) => this.logger.info('DOCUMENT', document)),
      );
  }


  public getDownloadTokenIdentificationDocument(enrolleeId: number, identificationDocumentId: number): Observable<string> {
    return this.apiResource
      .get<string>(`enrollees/${enrolleeId}/identification-document/${identificationDocumentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap((document: string) => this.logger.info('DOCUMENT', document)),
      );
  }

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleeVersionAdapterResponse(
    { id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileVersion
  ): EnrolmentProfileVersion {
    // Compensate for updates to the current enrolment model
    // that don't match enrolment versioning
    this.enrolleeVersionSnapshotAdapter(profileSnapshot);

    return {
      id,
      enrolleeId,
      profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
      createdDate
    };
  }

  private enrolleeVersionSnapshotAdapter(profileSnapshot: HttpEnrollee): void {
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
    // Fill in values that could be `null`
    if (!enrollee.mailingAddress) {
      enrollee.mailingAddress = new Address();
    }

    if (!enrollee.certifications) {
      enrollee.certifications = [];
    }

    if (!enrollee.jobs) {
      enrollee.jobs = [];
    }

    if (!enrollee.enrolleeCareSettings) {
      enrollee.enrolleeCareSettings = [];
    }

    if (!enrollee.enrolleeRemoteUsers) {
      enrollee.enrolleeRemoteUsers = [];
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
      physicalAddress,
      mailingAddress,
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
        physicalAddress,
        mailingAddress,
        email,
        smsPhone,
        phone,
        phoneExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      careSettings: enrollee.enrolleeCareSettings,
      enrolleeRemoteUsers: enrollee.enrolleeRemoteUsers,
      ...remainder
    };
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): HttpEnrollee {
    if (enrolment.enrollee.mailingAddress.postal) {
      enrolment.enrollee.mailingAddress.id = enrolment.enrollee.mailingAddress.id ?? 0;
      enrolment.enrollee.mailingAddress.postal = enrolment.enrollee.mailingAddress.postal.toUpperCase();
    } else {
      enrolment.enrollee.mailingAddress = null;
    }

    enrolment.certifications = this.removeIncompleteCollegeCertifications(enrolment.certifications);
    enrolment.jobs = this.removeIncompleteJobs(enrolment.jobs);
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
    const whitelist = ['practiceCode'];

    return Object.keys(certification)
      .every((key: string) =>
        (!whitelist.includes(key) && !certification[key]) ? certification[key] : true
      );
  }

  private removeIncompleteJobs(jobs: Job[]) {
    return jobs.filter((job: Job) => (job.title !== ''));
  }

  private removeIncompleteCareSettings(careSettings: CareSetting[]) {
    return careSettings.filter((careSetting: CareSetting) => careSetting.careSettingCode);
  }
}
