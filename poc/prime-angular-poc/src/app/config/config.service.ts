import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration, Config } from '@config/config.model';
import { AppHttpResponse } from '@core/models/app-http-response.model';

export interface IConfigService {
  config: Config<number>[];
  load(): Observable<Configuration>;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService implements IConfigService {
  protected configuration: Configuration;

  constructor(
    @Inject(APP_CONFIG) protected appConfig: AppConfig,
    protected http: HttpClient
  ) { }

  public get config(): Config<number>[] {
    return [...this.configuration.config]
      .sort(this.sortConfig);
  }

  /**
   * @description
   * Load the runtime configuration.
   */
  public load(): Observable<Configuration> {
    if (!this.configuration) {
      return this.getConfiguration()
        .pipe(
          map((config: Configuration) => this.configuration = config)
        );
    }

    return of(this.configuration);
  }

  /**
   * @description
   * Get the configuration for bootstrapping the application.
   */
  private getConfiguration(): Observable<Configuration> {
    return this.http.get<AppHttpResponse>(`${this.appConfig.apiEndpoint}/config`)
      .pipe(
        map((response: AppHttpResponse) => response.result)
      );
  }

  /**
   * @description
   * Sort configuration by name.
   */
  private sortConfig(item1: Config<number | string>, item2: Config<number | string>) {
    return (item1.name > item2.name)
      ? 1 : (item1.name < item2.name)
        ? -1 : 0;
  }
}
