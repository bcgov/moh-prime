import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';

// TODO move to lib
import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { DemographicForm } from '@sat/pages/demographic-page/demographic-form.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

@Injectable({
  providedIn: 'root'
})
export class SatEformsEnrolmentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public createSatEnrollee(enrollee: SatEnrollee): Observable<SatEnrollee> {
    return this.apiResource.post<SatEnrollee>('parties/sat', enrollee)
      .pipe(
        map((response: ApiHttpResponse<SatEnrollee>) => response.result),
        tap((enrollee: SatEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be created.');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::createSatEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSatEnrolleeByUserId(userId: string): Observable<SatEnrollee> {
    return this.apiResource.get<SatEnrollee>(`parties/sat/${userId}`)
      .pipe(
        map((response: ApiHttpResponse<SatEnrollee>) => response.result),
        tap((enrollee: SatEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          // Allow for creation of a new enrolment
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Enrollee could not be retrieved.');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::getSatEnrolleeByUserId error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSatEnrolleeById(enrolleeId: number): Observable<SatEnrollee> {
    return this.apiResource.get<SatEnrollee>(`parties/sat/${enrolleeId}`)
      .pipe(
        map((response: ApiHttpResponse<SatEnrollee>) => response.result),
        tap((enrollee: SatEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be retrieved.');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::getSatEnrolleeById error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateSatEnrollee(enrolleeId: number, enrollee: SatEnrollee): NoContent {
    return this.apiResource.put<NoContent>(`parties/sat/${enrolleeId}`, enrollee)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be updated');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::updateSatEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateSatEnrolleeCertifications(enrolleeId: number, certifications: CollegeCertification[]): NoContent {
    return this.apiResource.put<NoContent>(`parties/sat/${enrolleeId}/certifications`, certifications)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee certifications could not be updated');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::updateSatEnrolleeCertifications error has occurred: ', error);
          throw error;
        })
      );
  }

  public submitSatEnrollee(enrolleeId: number): NoContent {
    return this.apiResource.post<NoContent>(`parties/sat/${enrolleeId}/submissions`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be submitted');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::submitSatEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }
}
