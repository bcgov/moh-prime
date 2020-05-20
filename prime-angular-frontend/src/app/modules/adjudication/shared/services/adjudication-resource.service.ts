import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { Address } from '@shared/models/address.model';
import { AccessTerm } from '@shared/models/access-term.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { NoContent } from '@core/resources/abstract-resource';
import { EnrolmentStatusReference } from '@shared/models/enrolment-status-reference.model';
import { HttpParams } from '@angular/common/http';
import { Site } from '@registration/shared/models/site.model';

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

  public getEnrollees(textSearch?: string, statusCode?: number): Observable<HttpEnrollee[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ textSearch, statusCode });
    return this.apiResource.get<HttpEnrollee[]>('enrollees', params)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee[]>) => response.result),
        tap((enrollees: HttpEnrollee[]) => this.logger.info('ENROLLEES', enrollees)),
        map((enrollees: HttpEnrollee[]) => this.enrolleesAdapterResponse(enrollees)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolments could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrollees error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeById(enrolleeId: number, statusCode?: number): Observable<HttpEnrollee> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode });
    return this.apiResource.get<HttpEnrollee>(`enrollees/${enrolleeId}`, params)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeProfileVersions(enrolleeId: number): Observable<HttpEnrolleeProfileVersion[]> {
    return this.apiResource.get<HttpEnrolleeProfileVersion[]>(`enrollees/${enrolleeId}/versions`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeProfileVersion[]>) => response.result),
        tap((enrolleeProfileVersions: HttpEnrolleeProfileVersion[]) =>
          this.logger.info('ENROLLEE_PROFILE_VERSIONS', enrolleeProfileVersions)
        ),
        map((enrolleeProfileVersions: HttpEnrolleeProfileVersion[]) =>
          enrolleeProfileVersions.map(this.enrolleeVersionAdapterResponse())
        ),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee profile history could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeProfileVersions error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeProfileVersion(enrolleeId: number, enrolleeProfileVersionId: number): Observable<HttpEnrolleeProfileVersion> {
    return this.apiResource.get<HttpEnrolleeProfileVersion>(`enrollees/${enrolleeId}/versions/${enrolleeProfileVersionId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeProfileVersion>) => response.result),
        tap((enrolleeProfileVersion: HttpEnrolleeProfileVersion) =>
          this.logger.info('ENROLLEE_PROFILE_VERSION', enrolleeProfileVersion)
        ),
        map(this.enrolleeVersionAdapterResponse()),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee profile history could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeProfileVersion error has occurred: ', error);
          throw error;
        })
      );
  }

  public submissionAction(enrolleeId: number, action: SubmissionAction): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>(`enrollees/${enrolleeId}/submission/${action}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => {
          this.toastService.openErrorToast('Enrolment status has been updated');
          this.logger.info('UPDATED_ENROLLEE', enrollee);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be updated');
          this.logger.error('[Adjudication] AdjudicationResource::submissionAction error has occurred: ', error);
          throw error;
        })
      );
  }

  public setEnrolleeAdjudicator(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.put<HttpEnrollee>(`enrollees/${enrolleeId}/adjudicator`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        map((enrollee: HttpEnrollee) => enrollee),
        tap((enrollee: HttpEnrollee) => this.logger.info('UPDATED_ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be assigned');
          this.logger.error('[Adjudication] AdjudicationResource::setEnrolleeAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public removeEnrolleeAdjudicator(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${enrolleeId}/adjudicator`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        map((enrollee: HttpEnrollee) => enrollee),
        tap((enrollee: HttpEnrollee) => this.logger.info('UPDATED_ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator could not be unassigned');
          this.logger.error('[Adjudication] AdjudicationResource::removeEnrolleeAdjudicator error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEnrolleeAlwaysManual(enrolleeId: number, alwaysManual: boolean): NoContent {
    const url = `enrollees/${enrolleeId}/always-manual`;
    const request$ = (alwaysManual)
      ? this.apiResource.put<NoContent>(url, null)
      : this.apiResource.delete<NoContent>(url);

    return request$
      .pipe(
        // TODO remove pipe when ApiResource handles NoContent
        map(() => { }),
        tap(() => this.logger.info('UPDATED_ENROLLEE', alwaysManual)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be marked as always manual');
          this.logger.error('[Adjudication] AdjudicationResource::updateEnrolleeAlwaysManual error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeBusinessEvents(enrolleeId: number): Observable<BusinessEvent[]> {
    return this.apiResource.get<BusinessEvent[]>(`enrollees/${enrolleeId}/events`)
      .pipe(
        map((response: ApiHttpResponse<BusinessEvent[]>) => response.result),
        tap((businessEvents: BusinessEvent[]) =>
          this.logger.info('ENROLLEE_BUSINESS_EVENTS', businessEvents)
        ),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee business events could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolleeBusinessEvents error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteEnrollee(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.delete<HttpEnrollee>(`enrollees/${enrolleeId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => {
          this.toastService.openSuccessToast('Enrolment has been deleted');
          this.logger.info('DELETED_ENROLLEE', enrollee);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be deleted');
          this.logger.error('[Adjudication] AdjudicationResource::deleteEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendEnrolleeReminderEmail(enrolleeId: number): NoContent {
    return this.apiResource.post<NoContent>(`enrollees/${enrolleeId}/reminder`)
      .pipe(
        map(() => {
          this.toastService.openErrorToast('Enrollee reminder has been sent');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee reminder could not be sent');
          this.logger.error('[Enrolment] EnrolmentResource::sendReminderEmail error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdjudicatorNotes(enrolleeId: number): Observable<AdjudicationNote[]> {
    return this.apiResource.get(`enrollees/${enrolleeId}/adjudicator-notes`)
      .pipe(
        map((response: ApiHttpResponse<AdjudicationNote[]>) => response.result),
        tap((adjudicatorNotes: AdjudicationNote[]) => this.logger.info('ADJUDICATOR_NOTES', adjudicatorNotes)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudicator notes could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getAdjudicatorNotes error has occurred: ', error);
          throw error;
        })
      );
  }

  public createAdjudicatorNote(enrolleeId: number, note: string, link?: boolean): Observable<AdjudicationNote> {
    const payload = { data: note };
    let params = new HttpParams();
    if (link) {
      params = params.append('link', 'true');
    }
    return this.apiResource.post(`enrollees/${enrolleeId}/adjudicator-notes`, payload, params)
      .pipe(
        map((response: ApiHttpResponse<AdjudicationNote>) => response.result),
        tap((adjudicatorNote: AdjudicationNote) => {
          this.toastService.openErrorToast('Adjudication note has been saved');
          this.logger.info('NEW_ADJUDICATOR_NOTE', adjudicatorNote);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Adjudication note could not be saved');
          this.logger.error('[Adjudication] AdjudicationResource::createAdjudicatorNote error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAccessAgreementNote(
    enrolleeId: number,
    note: string
  ): Observable<AdjudicationNote> {
    const payload = { enrolleeId, note };
    return this.apiResource.put(`enrollees/${enrolleeId}/access-agreement-notes`, payload)
      .pipe(
        map((response: ApiHttpResponse<AdjudicationNote>) => response.result),
        tap((adjudicatorNote: AdjudicationNote) => {
          this.toastService.openSuccessToast(`Limits and conditions clause has been saved.`);
          this.logger.info('LIMITS_AND_CONDITIONS_CLAUSE', adjudicatorNote);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Limits and conditions clause could not be updated');
          this.logger.error('[Adjudication] AdjudicationResource::updateAccessAgreementNote error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Access Terms
  // ---

  public getAccessTerms(enrolleeId: number, year: number): Observable<AccessTerm[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ year });
    return this.apiResource.get<AccessTerm[]>(`enrollees/${enrolleeId}/access-terms`, params)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm[]>) => response.result),
        tap((accessTerms: AccessTerm[]) => this.logger.info('ACCESS_TERMS', accessTerms)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Access terms could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getAccessTerms error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAccessTerm(enrolleeId: number, accessTermsId: number): Observable<AccessTerm> {
    return this.apiResource.get(`enrollees/${enrolleeId}/access-terms/${accessTermsId}`)
      .pipe(
        map((response: ApiHttpResponse<AccessTerm>) => response.result),
        tap((accessTerm: AccessTerm) => this.logger.info('ACCESS_TERM', accessTerm)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getAccessTerm error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentForAccessTerm(enrolleeId: number, accessTermId: number)
    : Observable<HttpEnrolleeProfileVersion> {
    return this.apiResource.get(`enrollees/${enrolleeId}/access-terms/${accessTermId}/enrolment`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrolleeProfileVersion>) => response.result),
        tap((enrolleeProfileVersion: HttpEnrolleeProfileVersion) =>
          this.logger.info('ENROLLEE_PROFILE_VERSION', enrolleeProfileVersion)
        ),
        map(this.enrolleeVersionAdapterResponse()),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getEnrolmentForAccessTerm error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Admin
  // ---

  public createAdmin(admin: Admin): Observable<Admin> {
    return this.apiResource.post<Admin>('admins', admin)
      .pipe(
        map((response: ApiHttpResponse<Admin>) => response.result),
        tap((newAdmin: Admin) => this.logger.info('NEW_ADMIN', newAdmin)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::createAdmin error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdjudicators(): Observable<Admin[]> {
    return this.apiResource.get<Admin[]>('admins')
      .pipe(
        map((response: ApiHttpResponse<Admin[]>) => response.result),
        tap((admins: Admin[]) => this.logger.info('ADMINS', admins)),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::getAdjudicators error has occurred: ', error);
          throw error;
        })
      );
  }

  public createStatusAdjudicatorReference(enrolleeId: number): Observable<EnrolmentStatusReference> {
    return this.apiResource.post(`enrollees/${enrolleeId}/status-reference`)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentStatusReference>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Adjudication] AdjudicationResource::createStatusAdjudicatorReference error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Site Registration
  // ---
  public getSites(textSearch?: string, statusCode?: number): Observable<Site[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ textSearch, statusCode });
    return this.apiResource.get<Site[]>('sites', params)
      .pipe(
        map((response: ApiHttpResponse<Site[]>) => response.result),
        tap((sites: Site[]) => this.logger.info('SITES', sites)),
        map((sites: Site[]) => sites),
        catchError((error: any) => {
          this.toastService.openErrorToast('Sites could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteById(siteId: number, statusCode?: number): Observable<Site> {
    const params = this.apiResourceUtilsService.makeHttpParams({ statusCode });
    return this.apiResource.get<Site>(`sites/${siteId}`, params)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((site: Site) => this.logger.info('SITE', site)),
        map((site: Site) => site),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be retrieved');
          this.logger.error('[Adjudication] AdjudicationResource::getSiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSite(siteId: number): Observable<Site> {
    return this.apiResource.delete<Site>(`sites/${siteId}`)
      .pipe(
        map((response: ApiHttpResponse<Site>) => response.result),
        tap((site: Site) => {
          this.toastService.openSuccessToast('Site has been deleted');
          this.logger.info('DELETED_SITE', site);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be deleted');
          this.logger.error('[Adjudication] AdjudicationResource::deleteSite error has occurred: ', error);
          throw error;
        })
      );
  }

  // ---
  // Enrollee and Enrolment Adapters
  // ---

  private enrolleesAdapterResponse(enrollees: HttpEnrollee[]): HttpEnrollee[] {
    return enrollees.map((enrollee: HttpEnrollee): HttpEnrollee => this.enrolleeAdapterResponse(enrollee));
  }

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): HttpEnrollee {
    if (!enrollee.mailingAddress) {
      enrollee.mailingAddress = new Address();
    }

    if (!enrollee.certifications) {
      enrollee.certifications = [];
    }

    if (!enrollee.jobs) {
      enrollee.jobs = [];
    }

    if (!enrollee.enrolleeOrganizationTypes) {
      enrollee.enrolleeOrganizationTypes = [];
    }

    return enrollee;
  }

  private enrolleeVersionAdapterResponse():
    ({ id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileVersion) => HttpEnrolleeProfileVersion {
    return ({ id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileVersion) => ({
      id,
      enrolleeId,
      profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
      createdDate
    });
  }

}
