import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration, Config, PracticeConfig, CollegeConfig, LicenseConfig } from './config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';

export interface IConfigService {
  practices: PracticeConfig[];
  colleges: CollegeConfig[];
  countries: Config<string>[];
  jobNames: Config<number>[];
  licenses: LicenseConfig[];
  organizationNames: Config<number>[];
  organizationTypes: Config<number>[];
  provinces: Config<string>[];
  statuses: Config<number>[];
  load(): Observable<Configuration>;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService implements IConfigService {
  protected configuration: Configuration;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) { }

  public get practices(): PracticeConfig[] {
    return [...this.configuration.practices];
  }

  public get colleges(): CollegeConfig[] {
    return [...this.configuration.colleges];
  }

  public get countries(): Config<string>[] {
    return [...this.configuration.countries];
  }

  public get jobNames(): Config<number>[] {
    return [...this.configuration.jobNames];
  }

  public get licenses(): LicenseConfig[] {
    return [...this.configuration.licenses];
  }

  public get organizationNames(): Config<number>[] {
    return [...this.configuration.organizationNames];
  }

  public get organizationTypes(): Config<number>[] {
    return [...this.configuration.organizationTypes];
  }

  public get provinces(): Config<string>[] {
    return [...this.configuration.provinces];
  }

  public get statuses(): Config<number>[] {
    return [...this.configuration.statuses];
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
    return this.http.get<PrimeHttpResponse>(`${this.config.apiEndpoint}/lookups`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result)
      );
  }
}
