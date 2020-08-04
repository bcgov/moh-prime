import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Address } from '@shared/models/address.model';
import { AccessTerm } from '@shared/models/access-term.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { EnrolmentProfileVersion, HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { Organization } from '@enrolment/shared/models/organization.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';

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

  public createEnrollee(payload: Enrollee): Observable<Enrolment> {
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
      // TODO remove pipe when ApiResource handles NoContent
      .pipe(map(() => { }));
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
  // Access Terms
  // ---

  public getAccessTerms(enrolleeId: number): Observable<AccessTerm[]> {
    return this.apiResource.get<AccessTerm[]>(`enrollees/${enrolleeId}/access-terms`)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm[]>) => response.result),
        tap((accessTerms: AccessTerm[]) => this.logger.info('ACCESS_TERMS', accessTerms))
      );
  }

  public getAccessTerm(enrolleeId: number, accessTermsId: number): Observable<AccessTerm> {
    return this.apiResource.get<AccessTerm>(`enrollees/${enrolleeId}/access-terms/${accessTermsId}`)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm>) => response.result),
        tap((accessTerm: AccessTerm) => this.logger.info('ACCESS_TERM', accessTerm))
      );
  }

  public getAccessTermLatest(enrolleeId: number, signed: boolean): Observable<AccessTerm> {
    const params = new HttpParams({ fromObject: { signed: signed.toString() } });
    return this.apiResource.get<AccessTerm>(`enrollees/${enrolleeId}/access-terms/latest`, params)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm>) => response.result),
        tap((accessTerm: AccessTerm) => this.logger.info('ACCESS_TERM_LATEST', accessTerm))
      );
  }

  public getEnrolmentProfileForAccessTerm(enrolleeId: number, accessTermId: number): Observable<EnrolmentProfileVersion> {
    return this.apiResource.get<EnrolmentProfileVersion>(`enrollees/${enrolleeId}/access-terms/${accessTermId}/enrolment`)
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

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleeVersionAdapterResponse(
    { id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileVersion
  ): EnrolmentProfileVersion {
    return {
      id,
      enrolleeId,
      profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
      createdDate
    };
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

    if (!enrollee.enrolleeOrganizationTypes) {
      enrollee.enrolleeOrganizationTypes = [];
    }

    // Reorganize the shape of the enrollee into an enrolment
    // TODO refactor this adapter out of the application
    return this.enrolmentAdapter(enrollee);
  }

  private enrolmentAdapter(enrollee: HttpEnrollee): Enrolment {
    const {
      userId,
      firstName,
      middleName,
      lastName,
      preferredFirstName,
      preferredMiddleName,
      preferredLastName,
      dateOfBirth,
      gpid,
      hpdid,
      physicalAddress,
      mailingAddress,
      contactEmail,
      contactPhone,
      voicePhone,
      voiceExtension,
      ...remainder
    } = enrollee;

    return {
      enrollee: {
        userId,
        firstName,
        middleName,
        lastName,
        preferredFirstName,
        preferredMiddleName,
        preferredLastName,
        dateOfBirth,
        gpid,
        hpdid,
        physicalAddress,
        mailingAddress,
        contactEmail,
        contactPhone,
        voicePhone,
        voiceExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      organizations: enrollee.enrolleeOrganizationTypes,
      ...remainder
    };
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): HttpEnrollee {
    if (enrolment.enrollee.mailingAddress.postal) {
      enrolment.enrollee.mailingAddress.postal = enrolment.enrollee.mailingAddress.postal.toUpperCase();
    } else {
      enrolment.enrollee.mailingAddress = null;
    }

    enrolment.certifications = this.removeIncompleteCollegeCertifications(enrolment.certifications);
    enrolment.jobs = this.removeIncompleteJobs(enrolment.jobs);
    enrolment.organizations = this.removeIncompleteOrganizations(enrolment.organizations);

    return this.enrolleeAdapter(enrolment);
  }

  private enrolleeAdapter(enrolment: Enrolment): HttpEnrollee {
    const {
      enrollee,
      ...remainder
    } = enrolment;

    return {
      ...enrollee,
      enrolleeOrganizationTypes: enrolment.organizations,
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

  private removeIncompleteOrganizations(organizations: Organization[]) {
    return organizations.filter((organization: Organization) => organization.organizationTypeCode);
  }
}
