import { Injectable } from '@angular/core';
import { LoggerService } from '@core/services/logger.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { Observable } from 'rxjs';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { map, tap } from 'rxjs/operators';
import { ApiHttpResponse } from '@core/models/api-http-response.model';

@Injectable({
  providedIn: 'root'
})
export class HelpResource {

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private logger: LoggerService
  ) { }

  public enrolleeDisplayId(): Observable<number> {
    return this.apiResource.get<HttpEnrollee[]>('enrollees')
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee[]>) => response.result),
        tap((enrollees: HttpEnrollee[]) => this.logger.info('ENROLLEES', enrollees[0])),
        map((enrollees: HttpEnrollee[]) =>
          // Only a single enrollee will be provided
          (enrollees.length) ? enrollees.pop().displayId : null
        ),
      );
  }
}
