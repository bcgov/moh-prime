import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Config } from '@config/config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { Address } from '../models/address.model';
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
    return this.http.get(`${this.config.apiEndpoint}/enrolments`)
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
    return this.http.post(`${this.config.apiEndpoint}/enrolments`, this.enrolmentAdapterRequest(payload))
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
    return this.http.put(`${this.config.apiEndpoint}/enrolments/${id}`, this.enrolmentAdapterRequest(enrolment));
  }

  public updateEnrolmentStatus(id: number, statusCode: number): Observable<Config<number>[]> {
    const payload = { code: statusCode };
    return this.http.post(`${this.config.apiEndpoint}/enrolments/${id}/statuses`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((statuses: Config<number>[]) => {
          this.logger.info('ENROLMENT_STATUSES', statuses);
          return statuses;
        })
      );
  }

  public createEnrolmentCertificateAccessToken(userId: number): Observable<EnrolmentCertificateAccessToken> {
    const payload = { userId };
    return this.http.post(`${this.config.apiEndpoint}/enrolmentCertificate/access`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((token: EnrolmentCertificateAccessToken) => {
          this.logger.info('ACCESS_TOKEN', token);
          return token;
        })
      );
  }

  private enrolmentAdapterResponse(enrolment: Enrolment): Enrolment {
    if (!enrolment.enrollee.mailingAddress) {
      enrolment.enrollee.mailingAddress = new Address();
    }

    return enrolment;
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): Enrolment {
    if (enrolment.enrollee.physicalAddress.postal) {
      enrolment.enrollee.physicalAddress.postal = enrolment.enrollee.physicalAddress.postal.toUpperCase();
    }
    if (enrolment.enrollee.mailingAddress.postal) {
      enrolment.enrollee.mailingAddress.postal = enrolment.enrollee.mailingAddress.postal.toUpperCase();
    }

    return enrolment;
  }
}
