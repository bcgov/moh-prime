import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { Log } from '@core/models/log.model';


@Injectable({
  providedIn: 'root'
})
export class LoggerResource {
  constructor(
    private apiResource: ApiResource
  ) { }

  public createLog(log: Log): Observable<number> {
    return this.apiResource.post<number>(`client-logs`, log)
      .pipe(
        map((response: ApiHttpResponse<number>) => response.result),
      );
  }
}
