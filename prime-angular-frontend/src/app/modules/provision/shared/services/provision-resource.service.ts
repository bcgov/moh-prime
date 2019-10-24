import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Config } from '@config/config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class ProvisionResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public enrolments(statusCode?: number): Observable<Enrolment[]> {
    const params = (statusCode) ? { statusCode: `${statusCode}` } : {};
    return this.http.get(`${this.config.apiEndpoint}/enrolments`, { params })
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          return enrolments;
        })
      );
  }

  public updateEnrolmentStatus(id: number, statusCode: number): Observable<Config[]> {
    const payload = { code: statusCode };
    return this.http.post(`${this.config.apiEndpoint}/enrolments/${id}/statuses`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((statuses: Config[]) => {
          this.logger.info('ENROLMENT_STATUSES', statuses);
          return statuses;
        })
      );
  }

  public deleteEnrolment(id: number): Observable<Enrolment> {
    return this.http.delete(`${this.config.apiEndpoint}/enrolments/${id}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((enrolment: Enrolment) => {
          this.logger.info('ENROLMENT', enrolment);
          return enrolment;
        })
      );
  }
}
