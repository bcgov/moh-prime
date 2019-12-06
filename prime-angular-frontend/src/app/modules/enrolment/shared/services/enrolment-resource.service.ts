import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Config } from '@config/config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { Address } from '@enrolment/shared/models/address.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { Organization } from '@enrolment/shared/models/organization.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  // TODO X
  public api(): Observable<any> {
    return this.http.delete(`${this.config.apiEndpoint}/enrollees/4`);
  }

  public enrollee(): Observable<Enrolment> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrollees: HttpEnrollee[]) => this.logger.info('ENROLLEES', enrollees)),
        map((enrollees: HttpEnrollee[]) =>
          // Only a single enrollee will be provided
          (enrollees.length) ? this.enrolleeAdapterResponse(enrollees.pop()) : null
        )
      );
  }

  public createEnrollee(payload: Enrolment): Observable<Enrolment> {
    return this.http.post(`${this.config.apiEndpoint}/enrollees`, this.enrolmentAdapterRequest(payload))
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee))
      );
  }

  public updateEnrollee(enrolment: Enrolment): Observable<any> {
    const { id } = enrolment;
    return this.http.put(`${this.config.apiEndpoint}/enrollees/${id}`, this.enrolmentAdapterRequest(enrolment));
  }

  public updateEnrolmentStatus(id: number, statusCode: number): Observable<Config<number>[]> {
    const payload = { code: statusCode };
    return this.http.post(`${this.config.apiEndpoint}/enrollees/${id}/statuses`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as Config<number>[]),
        tap((statuses: Config<number>[]) => this.logger.info('ENROLMENT_STATUSES', statuses))
      );
  }

  public enrolmentCertificateAccessTokens(): Observable<EnrolmentCertificateAccessToken[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrolment-certificates/access`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((tokens: EnrolmentCertificateAccessToken[]) => this.logger.info('ACCESS_TOKENS', tokens))
      );
  }

  public createEnrolmentCertificateAccessToken(): Observable<EnrolmentCertificateAccessToken> {
    return this.http.post(`${this.config.apiEndpoint}/enrolment-certificates/access`, {})
      .pipe(
        map((response: PrimeHttpResponse) => response.result as EnrolmentCertificateAccessToken),
        tap((token: EnrolmentCertificateAccessToken) => this.logger.info('ACCESS_TOKEN', token))
      );
  }

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): Enrolment {
    if (!enrollee.mailingAddress) {
      enrollee.mailingAddress = new Address();
    }

    if (!enrollee.certifications) {
      enrollee.certifications = [];
    }

    if (!enrollee.jobs) {
      enrollee.jobs = [];
    }

    if (!enrollee.organizations) {
      enrollee.organizations = [];
    }

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
      licensePlate,
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
        licensePlate,
        physicalAddress,
        mailingAddress,
        contactEmail,
        contactPhone,
        voicePhone,
        voiceExtension
      },
      ...remainder
    };
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): HttpEnrollee {
    if (enrolment.enrollee.physicalAddress.postal) {
      enrolment.enrollee.physicalAddress.postal = enrolment.enrollee.physicalAddress.postal.toUpperCase();
    }
    if (enrolment.enrollee.mailingAddress.postal) {
      enrolment.enrollee.mailingAddress.postal = enrolment.enrollee.mailingAddress.postal.toUpperCase();
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
