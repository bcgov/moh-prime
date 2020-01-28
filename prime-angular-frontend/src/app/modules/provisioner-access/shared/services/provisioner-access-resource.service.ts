import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';

import { ProvisionerAccess } from '../models/provision-access.model';

@Injectable({
  providedIn: 'root'
})
export class ProvisionerAccessResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public getCertificate(accessTokenId: string): Observable<ProvisionerAccess> {
    return this.http.get(`${this.config.apiEndpoint}/provisioner-access/certificate/${accessTokenId}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as ProvisionerAccess),
        tap((enrolmentCertificate: ProvisionerAccess) => this.logger.info('ENROLMENT_CERTIFICATE', enrolmentCertificate))
      );
  }
}
