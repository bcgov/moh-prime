import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentCertificate } from '../models/enrolment-certificate.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentCertificateResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public getCertificate(accessTokenId: string): Observable<EnrolmentCertificate> {
    return this.http.get(`${this.config.apiEndpoint}/provisioner-access/certificate/${accessTokenId}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as EnrolmentCertificate),
        tap((enrolmentCertificate: EnrolmentCertificate) => this.logger.info('ENROLMENT_CERTIFICATE', enrolmentCertificate))
      );
  }
}
