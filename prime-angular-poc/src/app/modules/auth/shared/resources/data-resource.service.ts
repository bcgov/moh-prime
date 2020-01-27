import { Injectable, Inject } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { HttpClient } from '@angular/common/http';

import { map, tap } from 'rxjs/operators';

import { LoggerService } from '@core/services/logger.service';
import { AppHttpResponse } from '@core/models/app-http-response.model';

@Injectable({
  providedIn: 'root'
})
export class DataResource {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService
  ) { }

  public getData() {
    return this.http.get(`${this.config.apiEndpoint}/data`)
      .pipe(
        map((response: AppHttpResponse) => response.result),
        tap((response: any) => this.logger.info('DATA', response))
      );
  }
}
