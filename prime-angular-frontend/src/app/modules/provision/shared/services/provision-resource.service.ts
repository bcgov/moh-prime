import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';

// TODO: moved to SharedModule
import { Enrolment } from '@enrolment/shared/models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class ProvisionResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public enrolments(): Observable<Enrolment[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrolments`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        map((enrolments: Enrolment[]) => {
          this.logger.info('ENROLMENTS', enrolments);
          return enrolments;
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
