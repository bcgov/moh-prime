import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';

@Injectable({
  providedIn: 'root'
})
export class LoggerResource {
  constructor(
    private apiResource: ApiResource,
  ) { }

  public createErrorLog(log: string): Observable<void> {
    const payload = { data: log };
    return this.apiResource.post(`logs/error`, payload)
      .pipe(
        map((response: ApiHttpResponse<void>) => response.result),
      );
  }
}
