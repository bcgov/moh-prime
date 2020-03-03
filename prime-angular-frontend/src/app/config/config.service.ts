import { Injectable, Inject } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration, Config, PracticeConfig, CollegeConfig, LicenseConfig, ProvinceConfig, LicenseWeightedConfig } from './config.model';
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
      .sort(this.sortConfigByName.bind(this));
  }

  public get colleges(): CollegeConfig[] {
    return [...this.configuration.colleges]
      .sort(this.sortConfigByName.bind(this));
  }

  public get countries(): Config<string>[] {
    return [...this.configuration.countries]
      .sort(this.sortConfigByName.bind(this));
  }

  public get jobNames(): Config<number>[] {
    return [...this.configuration.jobNames]
      .sort(this.sortConfigByName.bind(this));
  }

  public get licenses(): LicenseConfig[] {
    return [...this.configuration.licenses]
      .sort(this.sortConfigByWeight.bind(this));
  }

  public get organizationTypes(): Config<number>[] {
    const communityPractice = this.configuration.organizationTypes
      .find(o => o.code === 2);

    return [...this.configuration.organizationTypes]
      .sort(this.sortConfigByName.bind(this))
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
      .sort(this.sortConfigByName.bind(this));
  }

  public get statuses(): Config<number>[] {
    return [...this.configuration.statuses]
      .sort(this.sortConfigByName.bind(this));
  }

  public get statusReasons() {
    return [...this.configuration.statusReasons]
      .sort(this.sortConfigByName.bind(this));
  }

  public get privilegeGroups() {
    return [...this.configuration.privilegeGroups]
      .sort(this.sortConfigByName.bind(this));
  }

  public get privilegeTypes() {
    return [...this.configuration.privilegeTypes]
      .sort(this.sortConfigByName.bind(this));
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
  private sortConfigByName(item1: Config<number | string>, item2: Config<number | string>) {
    return this.sortConfig<Config<number | string>>(item1, item2, 'name');
  }

  /**
   * @description
   * Sort the configuration by weight.
   */
  private sortConfigByWeight(item1: LicenseWeightedConfig, item2: LicenseWeightedConfig) {
    return this.sortConfig<LicenseWeightedConfig>(item1, item2, 'weight');
  }

  /**
   * @description
   * Generic sorting of a JSON object by key.
   */
  private sortConfig<T>(item1: T, item2: T, key: string) {
    return (item1[key] > item2[key])
      ? 1 : (item1[key] < item2[key])
        ? -1 : 0;
  }
}
