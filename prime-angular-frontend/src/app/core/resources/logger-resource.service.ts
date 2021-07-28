import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { Log } from '@core/models/log.model';

@Injectable({
  providedIn: 'root'
})
export class LoggerResource {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) { }

  public createLog(log: Log): Observable<number> {
    return this.http
      .post(`${this.config.apiEndpoint}/client-logs`, log, { observe: 'response' })
      .pipe(
        map((response: HttpResponse<any>) => response.body.result),
      );
  }
}
