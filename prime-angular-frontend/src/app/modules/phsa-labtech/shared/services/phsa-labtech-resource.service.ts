import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { PhsaLabtech } from '../models/phsa-lab-tech.model';

@Injectable({
  providedIn: 'root'
})
export class PhasLabtechResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createEnrollee(payload: PhsaLabtech): Observable<PhsaLabtech> {
    this.logger.trace('TODO: call API', payload);

    return this.apiResource.post<PhsaLabtech>('parties/labtechs', payload)
      .pipe(
        map((response: ApiHttpResponse<PhsaLabtech>) => response.result),
        tap((enrollee: PhsaLabtech) => this.logger.info('ENROLLEE', enrollee)),
//        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be created.');
          this.logger.error('[Enrolment] PhasLabtechResource::createEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }
}
