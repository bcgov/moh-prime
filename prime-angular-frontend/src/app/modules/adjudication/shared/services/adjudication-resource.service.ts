import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Config } from '@config/config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';

import { Address } from '@enrolment/shared/models/address.model';
import { NoteType } from '@adjudication/shared/enums/note-type.enum';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { EnrolmentProfileHistory, HttpEnrolleeProfileHistory } from '@adjudication/shared/models/enrollee-profile-history.model';

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
        map((enrollees: HttpEnrollee[]) => this.enrolleesAdapterResponse(enrollees))
      );
  }

  public enrollee(id: number, statusCode?: number): Observable<Enrolment> {
    const params = (statusCode) ? { statusCode: `${statusCode}` } : {};
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${id}`, { params })
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee))
      );
  }

  // TODO refactor not efficient but too many questions outstanding
  public enrolleeProfileHistories(enrolleeId: number): Observable<EnrolmentProfileHistory[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/history`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrolleeProfileHistories: HttpEnrolleeProfileHistory[]) =>
          this.logger.info('ENROLLEE_PROFILE_HISTORY', enrolleeProfileHistories)
        ),
        map((enrolleeProfileHistories: HttpEnrolleeProfileHistory[]) => {
          return enrolleeProfileHistories
            .map(({ id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileHistory) => {
              return {
                id,
                enrolleeId,
                profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
                createdDate
              } as EnrolmentProfileHistory;
            });
        })
      );
  }

  // TODO refactor not efficient but too many questions outstanding
  // TODO refactor to not require the enrollee prefix and only take /history/:id
  public enrolleeProfileHistory(enrolleeId: number, enrolleeProfileHistoryId: number): Observable<EnrolmentProfileHistory> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/history/${enrolleeProfileHistoryId}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrolleeProfileHistory: HttpEnrolleeProfileHistory) =>
          this.logger.info('ENROLLEE_PROFILE_HISTORY', enrolleeProfileHistory)
        ),
        map(({ id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileHistory) => {
          return {
            id,
            enrolleeId,
            profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
            createdDate
          } as EnrolmentProfileHistory;
        })
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

  public adjudicatorNotes(id: number): Observable<AdjudicationNote[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${id}/adjudicator-notes`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AdjudicationNote[]),
        tap((adjudicatorNotes: AdjudicationNote[]) => this.logger.info('ADJUDICATOR_NOTES', adjudicatorNotes))
      );
  }

  public addAdjudicatorNote(enrolleeId: number, note: string): Observable<AdjudicationNote> {
    const payload = { enrolleeId, note };
    return this.http.post(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/adjudicator-notes`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AdjudicationNote),
        tap((adjudicatorNote: AdjudicationNote) => this.logger.info('ADJUDICATOR_NOTE', adjudicatorNote))
      );
  }

  public updateAdjudicationNote(
    enrolleeId: number,
    note: string,
    noteType: NoteType.AccessAgreementNote | NoteType.EnrolmentCertificateNote
  ): Observable<AdjudicationNote> {
    const payload = { enrolleeId, note };
    const params = (noteType === NoteType.EnrolmentCertificateNote)
      ? { path: 'enrolment-certificate-notes', message: 'ENROLMENT_CERTIFICATE_NOTE' }
      : { path: 'access-agreement-notes', message: 'ACCESS_AGREEMENT_NOTE' };
    return this.http.put(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/${params.path}`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AdjudicationNote),
        tap((adjudicatorNote: AdjudicationNote) => this.logger.info(params.message, adjudicatorNote))
      );
  }

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleesAdapterResponse(enrollees: HttpEnrollee[]): Enrolment[] {
    return enrollees.map((enrollee: HttpEnrollee): Enrolment => this.enrolleeAdapterResponse(enrollee));
  }

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): Enrolment {
    if (!enrollee.mailingAddress) {
      enrollee.mailingAddress = new Address();
    }

    if (!enrollee.certifications) {
      enrollee.certifications = [];
    }

    if (!enrollee.jobs) {
      enrollee.jobs = [];
    }

    if (!enrollee.organizations) {
      enrollee.organizations = [];
    }

    return this.enrolmentAdapter(enrollee);
  }

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

    const collectionNoticeAccepted = false;

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
      collectionNoticeAccepted,
      ...remainder
    };
  }

  private enrolmentAdapterRequest(enrolment: Enrolment): HttpEnrollee {
    return this.enrolleeAdapter(enrolment);
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
