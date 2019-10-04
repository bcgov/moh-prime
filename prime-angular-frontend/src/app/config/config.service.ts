import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Config } from './config.model';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configuration: Config;

  constructor() { }

  public get advancedPractices() {
    return [...this.configuration.advancedPractices];
  }

  public get colleges() {
    return [...this.configuration.colleges];
  }

  public get countries() {
    return [...this.configuration.countries];
  }

  public get jobs() {
    return [...this.configuration.jobNames];
  }

  public get licenses() {
    return [...this.configuration.jobNames];
  }

  public get organization() {
    return [...this.configuration.organizationNames];
  }

  public get provinces() {
    return [...this.configuration.provinces];
  }

  /**
   * Load runtime configuration.
   *
   * @returns {Promise<Config>}
   * @memberof ConfigService
   */
  public async load(): Promise<Config> {
    return this.getConfiguration()
      .toPromise()
      .then(this.addColleges)
      .then(this.addJobNames)
      .then(this.addProvinces)
      .then(this.addCountries)
      .then((config) => this.configuration = config);
  }

  /**
   * Get the configuration for bootstrapping the application.
   *
   * @private
   * @returns {Observable<Config>}
   * @memberof ConfigService
   */
  private getConfiguration(): Observable<Config> {
    // TODO: add configuration endpoint /api/v1/Lookup
    return new Observable((subscriber) => {
      subscriber.complete();
    });
  }

  private addColleges(config: Config) {
    return {
      colleges: [
        { code: 'CRNBC', name: 'College of Registered Nurses of BC (CRNBC)' },
        { code: 'CPSBC', name: 'College of Physicians and Surgeons of BC (CPSBC)' },
        { code: 'CPBC', name: 'College of Pharmacists of BC (CPBC)' },
        { code: 'NONE', name: 'None' },
      ],
      ...config
    };
  }

  private addJobNames(config: Config) {
    return {
      jobNames: [
        { code: 'MEDOFFASS', name: 'Medical Office Assistant' },
        { code: 'MIDWIFE', name: 'Midwife' },
        { code: 'NURSE', name: 'Nurse (not Nurse Practitioner)' },
        { code: 'PHARMASST', name: 'Pharmacy Assistant' },
        { code: 'PHARMTECH', name: 'Pharmacy Technician' },
        { code: 'REGICLERK', name: 'Registration Clerk' },
        { code: 'WARDCLERK', name: 'Ward Clerk' },
        { code: 'OTHER', name: 'Other' }
      ],
      ...config
    };
  }

  private addProvinces(config: Config) {
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

  private addCountries(config: Config) {
    return {
      countries: [
        { code: 'CA', name: 'Canada' }
      ],
      ...config,
    };
  }
}
