import { Injectable, Inject } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import {
  Configuration, Config, PracticeConfig, CollegeConfig, ProvinceConfig,
  LicenseConfig, VendorConfig, CollegeLicenseGroupingConfig
} from '@config/config.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';

export interface IConfigService extends Configuration {
  load(): Observable<Configuration>;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService implements IConfigService {
  protected configuration: Configuration;

  constructor(
    protected apiResource: ApiResource,
    protected utilsService: UtilsService
  ) { }

  public get practices(): PracticeConfig[] {
    return [...this.configuration.practices]
      .sort(this.utilsService.sortByKey<PracticeConfig>('name'));
  }

  public get colleges(): CollegeConfig[] {
    return [...this.configuration.colleges]
      .sort(this.utilsService.sortByKey<CollegeConfig>('code'));
  }

  public get countries(): Config<string>[] {
    return [...this.configuration.countries]
      .sort(this.utilsService.sortByKey<Config<string>>('name'));
  }

  public get jobNames(): Config<number>[] {
    return [...this.configuration.jobNames]
      .sort(this.utilsService.sortByKey<Config<number>>('name'));
  }

  public get licenses(): LicenseConfig[] {
    return [...this.configuration.licenses]
      .sort(this.utilsService.sortByKey<LicenseConfig>('weight'));
  }

  public get careSettings(): Config<number>[] {
    const communityPractice = this.configuration.careSettings
      .find(o => o.code === 2);

    return [...this.configuration.careSettings]
      .sort(this.utilsService.sortByKey<Config<number>>('name'))
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
      .sort(this.utilsService.sortByKey<ProvinceConfig>('name'));
  }

  public get statuses(): Config<number>[] {
    return [...this.configuration.statuses]
      .sort(this.utilsService.sortByKey<Config<number>>('name'));
  }

  public get statusReasons(): Config<number>[] {
    return [...this.configuration.statusReasons]
      .sort(this.utilsService.sortByKey<Config<number>>('name'));
  }

  public get vendors(): VendorConfig[] {
    return [...this.configuration.vendors]
      .sort(this.utilsService.sortByKey<VendorConfig>('name'));
  }

  public get healthAuthorities(): Config<number>[] {
    return [...this.configuration.healthAuthorities]
      .sort(this.utilsService.sortByKey<Config<number>>('name'));
  }

  public get facilities(): Config<number>[] {
    return [...this.configuration.facilities]
      .sort(this.utilsService.sortByKey<Config<number>>('name'));
  }

  public get collegeLicenseGroupings(): CollegeLicenseGroupingConfig[] {
    return [...this.configuration.collegeLicenseGroupings]
      .sort(this.utilsService.sortByKey<CollegeLicenseGroupingConfig>('weight'));
  }

  public get careTypes(): Config<number>[] {
    return [...this.configuration.careTypes]
      .sort(this.utilsService.sortByKey<Config<number>>('name'));
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

    return of({ ...this.configuration });
  }

  /**
   * @description
   * Get the configuration for bootstrapping the application.
   */
  private getConfiguration(): Observable<Configuration> {
    return this.apiResource.get<Configuration>('lookups')
      .pipe(
        map((response: ApiHttpResponse<Configuration>) => response.result),
        map((configuration: Configuration) => {
          configuration.licenses
            .map((licenceConfig: LicenseConfig) => {
              // Nullable on backend, but converted to NA
              licenceConfig.prescriberIdType = licenceConfig.prescriberIdType ?? PrescriberIdTypeEnum.NA;
              return licenceConfig;
            });
          return configuration;
        }),
        catchError((error: any) => {
          // Catch and release to allow the application to render
          // views regardless of the presence of the lookups
          return of(null);
        })
      );
  }
}
