import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Config } from '@config/config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { Address } from '@enrolment/shared/models/address.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { Organization } from '@enrolment/shared/models/organization.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public enrolments(): Observable<Enrolment> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          const enrolment = (enrolments.length)
            ? this.enrolmentAdapterResponse(enrolments.shift())
            : null;

          return enrolment;
        })
      );
  }

  public createEnrolment(payload: Enrolment): Observable<Enrolment> {
    return this.http.post(`${this.config.apiEndpoint}/enrollees`, this.enrolmentAdapterRequest(payload))
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((enrolment: Enrolment) => {
          this.logger.info('ENROLMENT', enrolment);
          return this.enrolmentAdapterResponse(enrolment);
        })
      );
  }

  public updateEnrolment(enrolment: Enrolment): Observable<any> {
    const { id } = enrolment;
    return this.http.put(`${this.config.apiEndpoint}/enrollees/${id}`, this.enrolmentAdapterRequest(enrolment));
  }

  public updateEnrolmentStatus(id: number, statusCode: number): Observable<Config<number>[]> {
    const payload = { code: statusCode };
    return this.http.post(`${this.config.apiEndpoint}/enrollees/${id}/statuses`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((statuses: Config<number>[]) => {
          this.logger.info('ENROLMENT_STATUSES', statuses);
          return statuses;
        })
      );
  }

  public createEnrolmentCertificateAccessToken(): Observable<EnrolmentCertificateAccessToken> {
    return this.http.post(`${this.config.apiEndpoint}/enrolment-certificates/access`, {})
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((token: EnrolmentCertificateAccessToken) => {
          this.logger.info('ACCESS_TOKEN', token);
          return token;
        })
      );
  }

  public enrolmentCertificateAccessTokens(): Observable<EnrolmentCertificateAccessToken[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrolment-certificates/access`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((tokens: EnrolmentCertificateAccessToken[]) => {
          this.logger.info('ACCESS_TOKENS', tokens);
          return tokens;
        })
      );
  }

  private enrolmentAdapterResponse(enrolment: Enrolment): Enrolment {
    if (!enrolment.mailingAddress) {
      enrolment.mailingAddress = new Address();
    }

    return enrolment;
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): Enrolment {
    if (enrolment.physicalAddress.postal) {
      enrolment.physicalAddress.postal = enrolment.physicalAddress.postal.toUpperCase();
    }
    if (enrolment.mailingAddress.postal) {
      enrolment.mailingAddress.postal = enrolment.mailingAddress.postal.toUpperCase();
    }

    enrolment.certifications = this.removeIncompleteCollegeCertifications(enrolment.certifications);
    enrolment.jobs = this.removeIncompleteJobs(enrolment.jobs);
    enrolment.organizations = this.removeIncompleteOrganizations(enrolment.organizations);

    return enrolment;
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
