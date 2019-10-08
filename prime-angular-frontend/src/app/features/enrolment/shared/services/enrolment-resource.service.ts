import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '../models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentResourceService {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public enrolments(): Observable<Enrolment> {
    // TODO: add endpoint /api/v1/Enrolment/1
    return this.http.get(`${this.config.apiEndpoint}/enrolments`)
      .pipe(
        map((response: any) => {
          const enrolment = this.enrolmentAdapterResponse(response);
          this.logger.info('ENROLMENT', enrolment);
          return enrolment;
        })
      );
  }

  public createEnrolment(payload: any): Observable<Enrolment> {
    return this.http.post(`${this.config.apiEndpoint}/enrolments`, this.enrolmentAdapterRequest(payload))
      .pipe(
        map((response: any) => {
          const enrolment = this.enrolmentAdapterResponse(response);
          this.logger.info('ENROLMENT', enrolment);
          return enrolment;
        })
      );
  }

  public updateEnrolment(payload: any): Observable<Enrolment> {
    return this.http.put(`${this.config.apiEndpoint}/enrolements`, this.enrolmentAdapterRequest(payload))
      .pipe(
        map((response: any) => {
          const enrolment = this.enrolmentAdapterResponse(response);
          this.logger.info('ENROLMENT', enrolment);
          return enrolment;
        })
      );
  }

  private enrolmentAdapterResponse(enrolment: any): Enrolment {
    return enrolment;
  }

  private enrolmentAdapterRequest(enrolment: any) {
    return enrolment;
  }
}
