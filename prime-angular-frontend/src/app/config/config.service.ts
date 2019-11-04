import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { Configuration } from './config.model';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { map, catchError } from 'rxjs/operators';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configuration: Configuration;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) { }

  public get practices() {
    return [...this.configuration.practices];
  }

  public get colleges() {
    return [...this.configuration.colleges];
  }

  public get countries() {
    return [...this.configuration.countries];
  }

  public get jobNames() {
    return [...this.configuration.jobNames];
  }

  public get licenses() {
    return [...this.configuration.licenses];
  }

  public get organizationNames() {
    return [...this.configuration.organizationNames];
  }

  public get organizationTypes() {
    return [...this.configuration.organizationTypes];
  }

  public get provinces() {
    return [...this.configuration.provinces];
  }

  public get statuses() {
    return [...this.configuration.statuses];
  }

  public get statusReasons() {
    return [...this.configuration.statusReasons];
  }

  /**
   * @description
   * Load the runtime configuration.
   */
  public load(): Observable<Configuration> {
    if (!this.configuration) {
      return this.getConfiguration()
        .pipe(
          // TODO: temporary until provided by config service
          map(this.addProvinces),
          map(this.addCountries),
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

  // TODO: temporary until provided by config service
  private addProvinces(config: Configuration) {
    return {
      provinces: [
        { code: 'AB', name: 'Alberta' },
        { code: 'BC', name: 'British Columbia' },
        { code: 'MB', name: 'Manitoba' },
        { code: 'NB', name: 'New Brunswick' },
        { code: 'NL', name: 'Newfoundland and Labrador' },
        { code: 'NS', name: 'Nova Scotia' },
        { code: 'ON', name: 'Ontario' },
        { code: 'PE', name: 'Prince Edward Island' },
        { code: 'QC', name: 'Quebec' },
        { code: 'SK', name: 'Saskatchewan' },
        { code: 'NT', name: 'Northwest Territories' },
        { code: 'NU', name: 'Nunavut' },
        { code: 'YT', name: 'Yukon' }
      ],
      ...config
    };
  }

  // TODO: temporary until provided by config service
  private addCountries(config: Configuration) {
    return {
      countries: [
        { code: 'CA', name: 'Canada' }
      ],
      ...config,
    };
  }
}
