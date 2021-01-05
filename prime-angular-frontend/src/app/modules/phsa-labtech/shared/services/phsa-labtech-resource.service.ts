import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { PhsaEnrollee } from '../models/phsa-lab-tech.model';
import { PartyTypeEnum } from '@shared/enums/party-type.enum';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhsaLabtechResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createEnrollee(payload: PhsaEnrollee): Observable<PhsaEnrollee> {
    return this.apiResource.post<PhsaEnrollee>('parties/phsa', payload)
      .pipe(
        map((response: ApiHttpResponse<PhsaEnrollee>) => response.result),
        tap((enrollee: PhsaEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        tap((enrollee: PhsaEnrollee) => this.toastService.openSuccessToast('Enrolment information has been saved')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment could not be created.');
          this.logger.error('[Enrolment] PhasLabtechResource::createEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public getPreApprovals(email: string): Observable<PartyTypeEnum[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ email });
    return this.apiResource.get<PartyTypeEnum[]>('parties/phsa/pre-approved', params)
      .pipe(
        map((response: ApiHttpResponse<PartyTypeEnum[]>) => response.result),
        tap((result: PartyTypeEnum[]) => this.logger.info('RESULT', result)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Could not locate system access.');
          this.logger.error('PhasLabtechResource::getPreApprovals error has occurred: ', error);
          throw error;
        })
      );
  }


}
