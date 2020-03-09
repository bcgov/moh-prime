import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { AccessTerm } from '@shared/models/access-term.model';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentProfileVersion, HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { Admin } from '@auth/shared/models/admin.model';
import { Address } from '@enrolment/shared/models/address.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';

@Injectable({
  providedIn: 'root'
})
export class AdjudicationResource {

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createAdmin(payload: Admin): Observable<Admin> {
    return this.apiResource.post<Admin>('admins', payload)
      .pipe(
        map((response: ApiHttpResponse<Admin>) => response.result),
        tap((admin: Admin) => this.logger.info('ADMIN', admin)),
        map((admin: Admin) => admin)
      );
  }

  public getAdjudicators(): Observable<Admin[]> {
    return this.apiResource.get<Admin[]>('admins')
      .pipe(
        map((response: ApiHttpResponse<Admin[]>) => response.result),
        map((admins: Admin[]) => admins)
      );
  }

  public enrollees(statusCode?: number, textSearch?: string): Observable<Enrolment[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode, textSearch });
    return this.apiResource.get<HttpEnrollee[]>('enrollees', params)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee[]>) => response.result),
        tap((enrollees: HttpEnrollee[]) => this.logger.info('ENROLLEES', enrollees)),
        map((enrollees: HttpEnrollee[]) => this.enrolleesAdapterResponse(enrollees))
      );
  }

  public enrollee(id: number, statusCode?: number): Observable<Enrolment> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode });
    return this.apiResource.get<HttpEnrollee>(`enrollees/${id}`, params)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee))
      );
  }

  public enrolleeProfileVersions(enrolleeId: number): Observable<EnrolmentProfileVersion[]> {
    return this.apiResource.get<HttpEnrolleeProfileVersion[]>(`enrollees/${enrolleeId}/versions`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeProfileVersion[]>) => response.result),
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
    return this.apiResource.get<HttpEnrolleeProfileVersion>(`enrollees/${enrolleeId}/versions/${enrolleeProfileVersionId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeProfileVersion>) => response.result),
        tap((enrolleeProfileVersion: HttpEnrolleeProfileVersion) =>
          this.logger.info('ENROLLEE_PROFILE_VERSION', enrolleeProfileVersion)
        ),
        map(this.enrolleeVersionAdapterResponse.bind(this))
      );
  }

  public updateEnrolmentStatus(id: number, statusCode: number): Observable<Config<number>[]> {
    const payload = { code: statusCode };
    return this.apiResource.post<Config<number>[]>(`enrollees/${id}/statuses`, payload)
      .pipe(
        map((response: ApiHttpResponse<Config<number>[]>) => response.result),
        tap((statuses: Config<number>[]) => this.logger.info('ENROLMENT_STATUSES', statuses))
      );
  }

  public setEnrolleeAdjudicator(enrolleeId: number): Observable<Enrolment> {
    return this.apiResource.put<HttpEnrollee>(`enrollees/${enrolleeId}/adjudicator`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        map((enrollee: HttpEnrollee) => this.enrolmentAdapter(enrollee)),
        tap((enrolment: Enrolment) => this.logger.info('UPDATED_ENROLMENT', enrolment)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be assigned');
          this.logger.error('[Adjudication] AdjudicationResource::addEnrolleeAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeEnrolleeAdjudicator(enrolleeId: number): Observable<Enrolment> {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${enrolleeId}/adjudicator`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        map((enrollee: HttpEnrollee) => this.enrolmentAdapter(enrollee)),
        tap((enrolment: Enrolment) => this.logger.info('UPDATED_ENROLMENT', enrolment)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be unassigned');
          this.logger.error('[Adjudication] AdjudicationResource::removeEnrolleeAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEnrolleeAlwaysManual(id: number, alwaysManual: boolean): Observable<Enrolment> {
    const payload = { data: alwaysManual };
    return this.apiResource.patch(`enrollees/${id}/always-manual`, payload)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        map((enrollee: HttpEnrollee) => this.enrolmentAdapter(enrollee)),
        tap((enrolment: Enrolment) => this.logger.info('UPDATED_ENROLMENT', enrolment))
      );
  }

  public deleteEnrolment(id: number): Observable<Enrolment> {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${id}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolmentAdapter(enrollee))
      );
  }

  public adjudicatorNotes(id: number): Observable<AdjudicationNote[]> {
    return this.apiResource.get(`enrollees/${id}/adjudicator-notes`)
      .pipe(
        map((response: ApiHttpResponse<AdjudicationNote[]>) => response.result),
        tap((adjudicatorNotes: AdjudicationNote[]) => this.logger.info('ADJUDICATOR_NOTES', adjudicatorNotes))
      );
  }

  public addAdjudicatorNote(enrolleeId: number, note: string): Observable<AdjudicationNote> {
    const payload = { data: note };
    return this.apiResource.post(`enrollees/${enrolleeId}/adjudicator-notes`, payload)
      .pipe(
        map((response: ApiHttpResponse<AdjudicationNote>) => response.result),
        tap((adjudicatorNote: AdjudicationNote) => this.logger.info('ADJUDICATOR_NOTE', adjudicatorNote))
      );
  }

  public updateAdjudicationNote(
    enrolleeId: number,
    note: string
  ): Observable<AdjudicationNote> {
    const payload = { enrolleeId, note };
    return this.apiResource.put(`enrollees/${enrolleeId}/access-agreement-notes`, payload)
      .pipe(
        map((response: ApiHttpResponse<AdjudicationNote>) => response.result),
        tap((adjudicatorNote: AdjudicationNote) => this.logger.info('ACCESS_AGREEMENT_NOTE', adjudicatorNote))
      );
  }

  // ---
  // Access Terms
  // TODO: These are duplicated across resources.
  // ---

  public getAccessTerms(enrolleeId: number): Observable<AccessTerm[]> {
    return this.apiResource.get(`enrollees/${enrolleeId}/access-terms`)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm[]>) => response.result),
        tap((accessTerms: AccessTerm[]) => this.logger.info('ACCESS_TERM', accessTerms))
      );
  }

  public getAccessTerm(enrolleeId: number, id: number): Observable<AccessTerm> {
    return this.apiResource.get(`enrollees/${enrolleeId}/access-terms/${id}`)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm>) => response.result),
        tap((accessTerm: AccessTerm) => this.logger.info('ACCESS_TERM', accessTerm))
      );
  }

  public getEnrolmentProfileForAccessTerm(enrolleeId: number, accessTermId: number): Observable<EnrolmentProfileVersion> {
    return this.apiResource.get(`enrollees/${enrolleeId}/access-terms/${accessTermId}/enrolment`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentProfileVersion>) => response.result),
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
