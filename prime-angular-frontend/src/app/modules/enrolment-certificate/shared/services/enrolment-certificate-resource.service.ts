import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { EnrolmentCertificate } from '../models/enrolment-certificate.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentCertificateResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) { }

  public getCertificate(accessTokenId: string): Observable<EnrolmentCertificate> {
    return this.http.get(`${this.config.apiEndpoint}/enrolment-certificates/certificate/${accessTokenId}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((certificate: EnrolmentCertificate) => {
          //this.logger.info('ENROLMENTS', enrolments);

          return certificate;
        })
      );
  }
}
