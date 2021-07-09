import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoggerResource {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) { }

  public createErrorLog(log: { msg: string, data: string }): Observable<HttpResponse<any>> {
    return this.http
      .post(`${this.config.apiEndpoint}/logs/error`, log, { observe: 'response' });
  }

}
