import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';

import { LdapCredential } from '../models/ldap-credential.model';
import { GisEnrolment } from '../models/gis-enrolment.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

@Injectable({
  providedIn: 'root'
})
export class GisEnrolmentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public ldapLogin(credentials: LdapCredential): Observable<NoContent> {
    return this.apiResource.post<NoContent>('gis/ldap/login', credentials)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('You could not be authenticated.');
          this.logger.error('[GisModule] GisResource::ldapLogin error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolment(): Observable<GisEnrolment> {
    return this.apiResource.get<GisEnrolment>(`gis`)
      .pipe(
        map((response: ApiHttpResponse<GisEnrolment>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('');
          this.logger.error('[GisModule] GisResource::getEnrolment error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentById(gisId: number): Observable<GisEnrolment> {
    return this.apiResource.get<GisEnrolment>(`gis/${ gisId }`)
      .pipe(
        map((response: ApiHttpResponse<GisEnrolment>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('');
          this.logger.error('[GisModule] GisResource::getEnrolmentById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrolment(enrolment: GisEnrolment): Observable<GisEnrolment> {
    return this.apiResource.post<GisEnrolment>('gis', enrolment)
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
    return this.apiResource.put<NoContent>(`gis/${ enrolment.id }`, enrolment)
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
}
