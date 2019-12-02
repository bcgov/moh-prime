import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Config } from '@config/config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class AdjudicationResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public enrollees(statusCode?: number): Observable<Enrolment[]> {
    const params = (statusCode) ? { statusCode: `${statusCode}` } : {};
    return this.http.get(`${this.config.apiEndpoint}/enrollees`, { params })
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrollees: HttpEnrollee[]) => this.logger.info('ENROLLEES', enrollees)),
        map((enrollees: HttpEnrollee[]) => this.enrolmentsAdapter(enrollees))
      );
  }

  public enrollee(id: number, statusCode?: number): Observable<Enrolment> {
    const params = (statusCode) ? { statusCode: `${statusCode}` } : {};
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${id}`, { params })
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolmentAdapter(enrollee))
      );
  }

  public updateEnrolmentStatus(id: number, statusCode: number): Observable<Config<number>[]> {
    const payload = { code: statusCode };
    return this.http.post(`${this.config.apiEndpoint}/enrollees/${id}/statuses`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as Config<number>[]),
        tap((statuses: Config<number>[]) => this.logger.info('ENROLMENT_STATUSES', statuses))
      );
  }

  public deleteEnrolment(id: number): Observable<Enrolment> {
    return this.http.delete(`${this.config.apiEndpoint}/enrollees/${id}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolmentAdapter(enrollee))
      );
  }

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolmentsAdapter(enrollees: HttpEnrollee[]): Enrolment[] {
    return enrollees.map((enrollee: HttpEnrollee): Enrolment => this.enrolmentAdapter(enrollee));
  }

  private enrolmentAdapter(enrollee: HttpEnrollee): Enrolment {
    const {
      userId,
      firstName,
      middleName,
      lastName,
      preferredFirstName,
      preferredMiddleName,
      preferredLastName,
      dateOfBirth,
      licensePlate,
      physicalAddress,
      mailingAddress,
      contactEmail,
      contactPhone,
      voicePhone,
      voiceExtension,
      ...remainder
    } = enrollee;

    return {
      enrollee: {
        userId,
        firstName,
        middleName,
        lastName,
        preferredFirstName,
        preferredMiddleName,
        preferredLastName,
        dateOfBirth,
        licensePlate,
        physicalAddress,
        mailingAddress,
        contactEmail,
        contactPhone,
        voicePhone,
        voiceExtension
      },
      ...remainder
    };
  }

  private enrolleeAdapter(enrolment: Enrolment): HttpEnrollee {
    const {
      enrollee,
      ...remainder
    } = enrolment;

    return {
      ...enrollee,
      ...remainder
    };
  }
}
