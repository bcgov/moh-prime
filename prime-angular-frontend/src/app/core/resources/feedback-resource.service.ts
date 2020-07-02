import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiResource } from '@core/resources/api-resource.service';

import { Feedback } from '@shared/models/feedback.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbackResourceService {

  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createFeedback(payload: Feedback): Observable<Feedback> {
    return this.apiResource.post(`feedback`, payload)
      .pipe(
        map((response: ApiHttpResponse<Feedback>) => response.result),
        tap(() => this.toastService.openSuccessToast('Feedback has been submitted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Feedback could not be submitted');
          this.logger.error('[Enrolment] FeedbackResourceService::createFeedback error has occurred: ', error);
          throw error;
        })
      );
  }
}
