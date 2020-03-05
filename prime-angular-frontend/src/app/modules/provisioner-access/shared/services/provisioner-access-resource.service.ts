import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';

import { EnrolmentCertificate } from '../models/enrolment-certificate.model';

@Injectable({
  providedIn: 'root'
})
export class ProvisionerAccessResource {

  constructor(
    private apiResource: ApiResource,
    private logger: LoggerService
  ) { }

  public getCertificate(accessTokenId: string): Observable<EnrolmentCertificate> {
    return this.apiResource.get(`provisioner-access/certificate/${accessTokenId}`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCertificate>) => response.result as EnrolmentCertificate),
        tap((enrolmentCertificate: EnrolmentCertificate) => this.logger.info('ENROLMENT_CERTIFICATE', enrolmentCertificate))
      );
  }
}
