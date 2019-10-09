import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import * as moment from 'moment';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '../models/enrolment.model';
import { Address } from '../models/address.model';
import { EnrolmentStateService } from './enrolment-state.service';


@Injectable({
  providedIn: 'root'
})
export class EnrolmentResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService,
    // TODO: temporary enrolment data provided until JWT authentication is in place
    private enrolmentStateService: EnrolmentStateService
  ) { }

  public enrolments(): Observable<Enrolment> {
    return this.http.get(`${this.config.apiEndpoint}/enrolments`)
      .pipe(
        // TODO: temporary enrolment data provided until JWT authentication is in place
        // map((enrolments: Enrolment[]) => [this.enrolmentStateService.getRawEnrolment()]),
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
        map((enrolment: Enrolment) => {
          this.logger.info('ENROLMENT', enrolment);
          return this.enrolmentAdapterResponse(this.enrolmentAdapterResponse(enrolment));
        })
      );
  }

  // TODO: revisit response for an enrolment update if applicable
  public updateEnrolment(enrolment: Enrolment): Observable<any> {
    const { id } = enrolment;
    return this.http.put(`${this.config.apiEndpoint}/enrolments/${id}`, this.enrolmentAdapterRequest(enrolment));
  }

  private enrolmentAdapterResponse(enrolment: Enrolment): Enrolment {
    if (!enrolment.enrollee.mailingAddress) {
      enrolment.enrollee.mailingAddress = new Address();
    }

    return enrolment;
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): Enrolment {
    // TODO: set postal code to be uppercase
    // TODO: temporary placeholder for birthdate until JWT authorization in place
    if (!enrolment.enrollee.dateOfBirth) {
      enrolment.enrollee.dateOfBirth = moment().toISOString();
    }

    return enrolment;
  }
}
