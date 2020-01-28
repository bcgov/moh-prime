import { Injectable, Inject } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
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

  public getGpid(): Observable<string> {
    return this.http.get(`${this.config.apiEndpoint}/provisioner-access/gpid`)
      .pipe(
        map((response: AppHttpResponse) => response.result as string),
        tap((gpid: string) => this.logger.info('GPID', gpid))
      );
  }
}
