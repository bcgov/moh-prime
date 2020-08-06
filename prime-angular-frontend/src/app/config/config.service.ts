import { Injectable, Inject } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration, Config, PracticeConfig, CollegeConfig, ProvinceConfig, LicenseWeightedConfig } from '@config/config.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { UtilsService, SortWeight } from '@core/services/utils.service';

export interface IConfigService extends Configuration {
  load(): Observable<Configuration>;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService implements IConfigService {
  protected configuration: Configuration;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig,
    protected apiResource: ApiResource,
    protected utilsService: UtilsService
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

  public get licenses(): LicenseWeightedConfig[] {
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

  public get vendors() {
    return [...this.configuration.vendors]
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
      this.utilsService.sortByKey<Config<number | string>>(a, b, 'name');
  }

  /**
   * @description
   * Sort the configuration by weight.
   */
  private sortConfigByWeight(): (a: LicenseWeightedConfig, b: LicenseWeightedConfig) => SortWeight {
    return (a: LicenseWeightedConfig, b: LicenseWeightedConfig) =>
      this.utilsService.sortByKey<LicenseWeightedConfig>(a, b, 'weight');
  }
}
