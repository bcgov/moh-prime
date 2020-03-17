import { Injectable } from '@angular/core';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { Observable } from 'rxjs';
import { Feedback } from '@shared/components/dialogs/content/feedback/feedback.component';
import { tap, map } from 'rxjs/operators';
import { ApiHttpResponse } from '@core/models/api-http-response.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbackResourceService {

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createFeedback(payload: Feedback): Observable<Feedback> {
    return this.apiResource.post(`feedback`, payload)
      .pipe(
        map((response: ApiHttpResponse<Feedback>) => response.result),
        tap((feedbackReturn: Feedback) => this.logger.info('FEEDBACK', feedbackReturn))
      );
  }
}
