import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

@Injectable({
  providedIn: 'root'
})
export class HelpResource {
  constructor(
    private apiResource: ApiResource,
    private logger: ConsoleLoggerService
  ) { }

  public enrolleeDisplayId(): Observable<number> {
    return this.apiResource.get<HttpEnrollee[]>('enrollees')
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee[]>) => response.result),
        tap((enrollees: HttpEnrollee[]) => this.logger.info('ENROLLEES', enrollees[0])),
        map((enrollees: HttpEnrollee[]) =>
          // Only a single enrollee will be provided
          (enrollees.length) ? enrollees.pop().displayId : null
        )
      );
  }
}
