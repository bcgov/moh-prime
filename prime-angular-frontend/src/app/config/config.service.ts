import { Injectable, Inject } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import {
  Configuration, Config, PracticeConfig, CollegeConfig,
  LicenseConfig, ProvinceConfig, LicenseWeightedConfig
} from '@config/config.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';

export interface IConfigService {
  practices: PracticeConfig[];
  colleges: CollegeConfig[];
  countries: Config<string>[];
  jobNames: Config<number>[];
  licenses: LicenseConfig[];
  organizationTypes: Config<number>[];
  provinces: ProvinceConfig[];
  statuses: Config<number>[];
  privilegeGroups: Config<number>[];
  privilegeTypes: Config<number>[];
  load(): Observable<Configuration>;
}

export type SortWeight = -1 | 0 | 1;

@Injectable({
  providedIn: 'root'
})
export class ConfigService implements IConfigService {
  protected configuration: Configuration;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig,
    protected apiResource: ApiResource
  ) { }

  public get practices(): PracticeConfig[] {
    return [...this.configuration.practices]
      .sort(this.sortConfigByName());
  }

  public get colleges(): CollegeConfig[] {
    return [...this.configuration.colleges]
      .sort(this.sortConfigByName());
  }

  public get countries(): Config<string>[] {
    return [...this.configuration.countries]
      .sort(this.sortConfigByName());
  }

  public get jobNames(): Config<number>[] {
    return [...this.configuration.jobNames]
      .sort(this.sortConfigByName());
  }

  public get licenses(): LicenseConfig[] {
    return [...this.configuration.licenses]
      .sort(this.sortConfigByWeight());
  }

  public get organizationTypes(): Config<number>[] {
    const communityPractice = this.configuration.organizationTypes
      .find(o => o.code === 2);

    return [...this.configuration.organizationTypes]
      .sort(this.sortConfigByName())
      // Move community practice to the top
      // TODO remove after community practice
      .filter(o => o.code !== 2)
      .reduce((os, o) => {
        os.push(o);
        return os;
      }, [communityPractice]);
  }

  public get provinces(): ProvinceConfig[] {
    return [...this.configuration.provinces]
      .sort(this.sortConfigByName());
  }

  public get statuses(): Config<number>[] {
    return [...this.configuration.statuses]
      .sort(this.sortConfigByName());
  }

  public get statusReasons() {
    return [...this.configuration.statusReasons]
      .sort(this.sortConfigByName());
  }

  public get privilegeGroups() {
    return [...this.configuration.privilegeGroups]
      .sort(this.sortConfigByName());
  }

  public get privilegeTypes() {
    return [...this.configuration.privilegeTypes]
      .sort(this.sortConfigByName());
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
    return this.apiResource.get<Configuration>('lookups')
      .pipe(
        map((response: ApiHttpResponse<Configuration>) => response.result)
      );
  }

  /**
   * @description
   * Sort the configuration by name.
   */
  private sortConfigByName(): (a: Config<number | string>, b: Config<number | string>) => SortWeight {
    return (a: Config<number | string>, b: Config<number | string>) =>
      this.sortConfig<Config<number | string>>(a, b, 'name');
  }

  /**
   * @description
   * Sort the configuration by weight.
   */
  private sortConfigByWeight(): (a: LicenseWeightedConfig, b: LicenseWeightedConfig) => SortWeight {
    return (a: LicenseWeightedConfig, b: LicenseWeightedConfig) =>
      this.sortConfig<LicenseWeightedConfig>(a, b, 'weight');
  }

  /**
   * @description
   * Generic sorting of a JSON object by key.
   */
  private sortConfig<T>(a: T, b: T, key: string): SortWeight {
    return (a[key] > b[key])
      ? 1 : (a[key] < b[key])
        ? -1 : 0;
  }
}
