import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

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
import { EnrolmentProfileVersion, HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';
import { AccessTerm } from '@shared/models/access-term.model';

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

  public enrolleeProfileVersions(enrolleeId: number): Observable<EnrolmentProfileVersion[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/versions`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrolleeProfileVersions: HttpEnrolleeProfileVersion[]) =>
          this.logger.info('ENROLLEE_PROFILE_VERSIONS', enrolleeProfileVersions)
        ),
        map((enrolleeProfileHistories: HttpEnrolleeProfileVersion[]) => {
          return enrolleeProfileHistories
            .map(this.enrolleeVersionAdapterResponse.bind(this));
        })
      );
  }

  // TODO located in EnrolleeController, which is prefixed with enrollee, but actually should just be /versions/${id}
  public enrolleeProfileVersion(enrolleeId: number, enrolleeProfileVersionId: number): Observable<EnrolmentProfileVersion> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/versions/${enrolleeProfileVersionId}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result),
        tap((enrolleeProfileVersion: HttpEnrolleeProfileVersion) =>
          this.logger.info('ENROLLEE_PROFILE_VERSION', enrolleeProfileVersion)
        ),
        map(this.enrolleeVersionAdapterResponse.bind(this))
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

  public updateEnrolleeAlwaysManual(id: number, alwaysManual: boolean): Observable<any> {
    const payload = { data: alwaysManual };
    return this.http.patch(`${this.config.apiEndpoint}/enrollees/${id}/always-manual`, payload);
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
    const payload = { data: note };
    return this.http.post(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/adjudicator-notes`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AdjudicationNote),
        tap((adjudicatorNote: AdjudicationNote) => this.logger.info('ADJUDICATOR_NOTE', adjudicatorNote))
      );
  }

  public updateAdjudicationNote(
    enrolleeId: number,
    note: string
  ): Observable<AdjudicationNote> {
    const payload = { enrolleeId, note };
    return this.http.put(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/access-agreement-notes`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AdjudicationNote),
        tap((adjudicatorNote: AdjudicationNote) => this.logger.info('ACCESS_AGREEMENT_NOTE', adjudicatorNote))
      );
  }

  public updateAlwaysManualFlag(enrolleeId: number, alwaysManual: boolean): Observable<Config<boolean>[]> {
    const payload = { enrolleeId, alwaysManual };
    return this.http.post(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/alwaysManual`, payload)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as Config<boolean>[]),
        tap((alwaysManual: Config<boolean>[]) => this.logger.info('ALWAYS_MANUAL', alwaysManual))
      );
  }

  // ---
  // Access Terms
  // TODO: These are duplicated across resources.
  // ---

  public getAccessTerms(enrolleeId: number): Observable<AccessTerm[]> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/access-terms`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AccessTerm[]),
        tap((accessTerms: AccessTerm[]) => this.logger.info('ACCESS_TERM', accessTerms))
      );
  }

  public getAccessTerm(enrolleeId: number, id: number): Observable<AccessTerm> {
    return this.http.get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/access-terms/${id}`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as AccessTerm),
        tap((accessTerm: AccessTerm) => this.logger.info('ACCESS_TERM', accessTerm))
      );
  }

  public getEnrolmentProfileForAccessTerm(enrolleeId: number, accessTermId: number): Observable<EnrolmentProfileVersion> {
    return this.http
      .get(`${this.config.apiEndpoint}/enrollees/${enrolleeId}/access-terms/${accessTermId}/enrolment`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result as EnrolmentProfileVersion),
        tap((enrolmentProfileVersion: EnrolmentProfileVersion) => this.logger.info('ENROLMENT_PROFILE_VERSION', enrolmentProfileVersion)),
        map(this.enrolleeVersionAdapterResponse.bind(this))
      );
  }

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleeVersionAdapterResponse(
    { id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileVersion
  ): EnrolmentProfileVersion {
    return {
      id,
      enrolleeId,
      profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
      createdDate
    };
  }

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
      gpid,
      hpdid,
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
        gpid,
        hpdid,
        physicalAddress,
        mailingAddress,
        contactEmail,
        contactPhone,
        voicePhone,
        voiceExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
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
