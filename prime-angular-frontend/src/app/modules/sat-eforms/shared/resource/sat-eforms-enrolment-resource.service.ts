import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

// TODO move to lib
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { DemographicForm } from '@paper-enrolment/pages/demographic-page/demographic-form.model';

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

  public getEnrolleeById(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.get<HttpEnrollee>(`enrollees/${enrolleeId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          return of(null);

          this.logger.error('[SatEforms] SatEformsEnrolmentResource::getEnrolleeById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrollee(payload: DemographicForm): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>('enrollees/sat-eforms', payload)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          return of(null);

          this.toastService.openErrorToast('Enrollee could not be created.');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::createEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateDemographic(enrolleeId: number, demographic: DemographicForm): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/sat-eforms/demographics`, demographic)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          return of(null);

          this.toastService.openErrorToast('SAT e-Forms enrolment demographic could not be updated');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::updateDemographic error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateCertifications(enrolleeId: number, certifications: CollegeCertification[]): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/sat-eforms/certifications`, certifications)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          return of(null);

          this.toastService.openErrorToast('SAT e-Forms enrolment certifications could not be updated');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::updateCertifications error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Finalize the paper enrolment submission.
   */
  public finalize(enrolleeId: number): NoContent {
    return this.apiResource.post<NoContent>(`enrollees/${enrolleeId}/sat-eforms/finalize`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          return of(null);

          this.toastService.openErrorToast('SAT e-Forms enrolment could not be finalized');
          this.logger.error('[SatEforms] SatEformsEnrolmentResource::finalize error has occurred: ', error);
          throw error;
        })
      );
  }
}
