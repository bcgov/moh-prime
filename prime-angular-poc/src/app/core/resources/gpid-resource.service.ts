import { Injectable, Inject } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { HttpClient } from '@angular/common/http';

import { map, tap } from 'rxjs/operators';

import { LoggerService } from '@core/services/logger.service';
import { AppHttpResponse } from '@core/models/app-http-response.model';

@Injectable({
  providedIn: 'root'
})
export class GpidResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public getGpid() {
    return this.http.get(`${this.config.apiEndpoint}/enrolment-certificates/gpid`)
      .pipe(
        map((response: AppHttpResponse) => response.result),
        tap((response: any) => this.logger.info('GPID', response))
      );
  }
}
