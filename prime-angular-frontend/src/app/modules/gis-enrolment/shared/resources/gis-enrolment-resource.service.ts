import { Injectable } from '@angular/core';

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
import { LdapThrottlingParameters } from '../models/ldap-error.model';

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

  public ldapLogin(enrolmentId: number, credentials: LdapCredential): Observable<any> {
    return this.apiResource.post<NoContent | LdapThrottlingParameters>(`parties/gis/${enrolmentId}/ldap/login`, credentials)
      .pipe(
        // NoContentResponse,
        // catchError((error: any) => {
        //   this.toastService.openErrorToast('You could not be authenticated.');
        //   this.logger.error('[GisModule] GisResource::ldapLogin error has occurred: ', error);
        //   console.log('Response Headers: ', error.headers.has('RemainingAttempts'));
        //   // NOTE check status
        //   // What do you need to do to get the headers?
        //   if (error.status === 401) {
        //     console.log('Response Headers: ', error.headers.get('RemainingAttempts'));
        //     // return of({ remainingAttempts: error.headers.get('RemainingAttempts'), lockoutTimeInHours: error.headers.get('lockoutTimeInHours') });
        // const ldapError = {
        //   remainingAttempts: error.headers.get('RemainingAttempts'),
        //   lockoutTimeInHours: error.headers.get('LockoutTimeInHours')
        // };
        //     return of(ldapError);
        //   }
        //   // return error;
        //   throw error;
        // })
        catchError((error) => of({
          remainingAttempts: 1,
          lockoutTimeInHours: 4
        }))
      )
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
