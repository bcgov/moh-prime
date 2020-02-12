import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration, Config, PracticeConfig, CollegeConfig, LicenseConfig, ProvinceConfig } from './config.model';
import { PrimeHttpResponse } from '@core/models/prime-http-response.model';

export interface IConfigService {
  practices: PracticeConfig[];
  colleges: CollegeConfig[];
  countries: Config<string>[];
  jobNames: Config<number>[];
  licenses: LicenseConfig[];
  organizationNames: Config<number>[];
  organizationTypes: Config<number>[];
  provinces: ProvinceConfig[];
  statuses: Config<number>[];
  privilegeGroups: Config<number>[];
  privilegeTypes: Config<number>[];
  load(): Observable<Configuration>;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService implements IConfigService {
  protected configuration: Configuration;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig,
    protected http: HttpClient
  ) { }

  public get practices(): PracticeConfig[] {
    return [...this.configuration.practices]
      .sort(this.sortConfig);
  }

  public get colleges(): CollegeConfig[] {
    return [...this.configuration.colleges]
      .sort(this.sortConfig);
  }

  public get countries(): Config<string>[] {
    return [...this.configuration.countries]
      .sort(this.sortConfig);
  }

  public get jobNames(): Config<number>[] {
    return [...this.configuration.jobNames]
      .sort(this.sortConfig);
  }

  public get licenses(): LicenseConfig[] {
    return [...this.configuration.licenses]
      .sort(this.sortConfigWeight);
  }

  public get organizationNames(): Config<number>[] {
    return [...this.configuration.organizationNames]
      .sort(this.sortConfig);
  }

  public get organizationTypes(): Config<number>[] {
    return [...this.configuration.organizationTypes]
      .sort(this.sortConfig);
  }

  public get provinces(): ProvinceConfig[] {
    return [...this.configuration.provinces]
      .sort(this.sortConfig);
  }

  public get statuses(): Config<number>[] {
    return [...this.configuration.statuses]
      .sort(this.sortConfig);
  }

  public get statusReasons() {
    return [...this.configuration.statusReasons]
      .sort(this.sortConfig);
  }

  public get privilegeGroups() {
    return [...this.configuration.privilegeGroups]
      .sort(this.sortConfig);
  }

  public get privilegeTypes() {
    return [...this.configuration.privilegeTypes]
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
    return this.http.get<PrimeHttpResponse>(`${this.config.apiEndpoint}/lookups`)
      .pipe(
        map((response: PrimeHttpResponse) => response.result)
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

  /**
   * @description
   * Sort configuration by weight.
   */
  private sortConfigWeight(item1: LicenseConfig, item2: LicenseConfig) {
    return (item1.weight > item2.weight)
      ? 1 : (item1.weight < item2.weight)
        ? -1 : 0;
  }

  /**
   *  @description
   *  Filter config items that contain the value to be matched in their name
   *  to the bottom of the list.
   */
  private filterBottom(list: Config<number | string>[], match: string) {
    return list.reduce((acc, item) => {
      (item.name.includes(match)) ? acc[1].push(item) : acc[0].push(item);
      return acc;
    }, [[], []]).reduce((acc, temp) =>
      acc.concat(temp)
      , []);
  }

}
