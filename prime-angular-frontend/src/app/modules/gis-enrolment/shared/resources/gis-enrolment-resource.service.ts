import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ToastService } from '@core/services/toast.service';

import { LdapCredential } from '../models/ldap-credential.model';
import { GisEnrolment } from '../models/gis-enrolment.model';
import { LdapErrorResponse } from '../models/ldap-error-response.model';

@Injectable({
  providedIn: 'root'
})
export class GisEnrolmentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public ldapLogin(enrolmentId: number, credentials: LdapCredential): Observable<NoContent> {
    return this.apiResource.post<NoContent>(`parties/gis/${enrolmentId}/ldap/login`, credentials)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('You could not be authenticated.');
          this.logger.error('[GisModule] GisResource::ldapLogin error has occurred: ', error);

          if (error.status === 401) {
            const remainingAttempts = +error.headers.get('RemainingAttempts');
            const lockoutTimeInHours = +error.headers.get('LockoutTimeInHours');
            return of(new LdapErrorResponse(remainingAttempts, lockoutTimeInHours));
          }

          throw error;
        })
      );
  }

  public getEnrolmentByUserId(userId: string): Observable<GisEnrolment> {
    return this.apiResource.get<GisEnrolment>(`parties/gis/${userId}`)
      .pipe(
        map((response: ApiHttpResponse<GisEnrolment>) => response.result),
        catchError((error: any) => {
          // Allow for creation of a new enrolment
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[GisModule] GisResource::getEnrolment error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentById(enrolmentId: number): Observable<GisEnrolment> {
    return this.apiResource.get<GisEnrolment>(`parties/gis/${enrolmentId}`)
      .pipe(
        map((response: ApiHttpResponse<GisEnrolment>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[GisModule] GisResource::getEnrolmentById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrolment(enrolment: GisEnrolment): Observable<GisEnrolment> {
    return this.apiResource.post<GisEnrolment>('parties/gis', enrolment)
      .pipe(
        map((response: ApiHttpResponse<GisEnrolment>) => response.result),
        tap((newEnrolment: GisEnrolment) => {
          this.toastService.openSuccessToast('Enrolment has been created');
          this.logger.info('NEW_ENROLMENT', newEnrolment);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be created');
          this.logger.error('[GisModule] GisResource::createEnrolment error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEnrolment(enrolment: GisEnrolment): NoContent {
    return this.apiResource.put<NoContent>(`parties/gis/${enrolment.id}`, enrolment)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Enrolment has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be updated.');
          this.logger.error('[GisModule] GisResource::updateEnrolment error has occurred: ', error);
          throw error;
        })
      );
  }

  public submission(enrolmentId: number): NoContent {
    return this.apiResource.post<NoContent>(`parties/gis/${enrolmentId}/submission`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Enrolment has been submitted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be submitted.');
          this.logger.error('[GisModule] GisResource::submission error has occurred: ', error);
          throw error;
        })
      );
  }
}
