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

  public get jobNames() {
    return [...this.configuration.jobNames];
  }

  public get licenses() {
    return [...this.configuration.licenses];
  }

  public get organizationNames() {
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
      // TODO: temporary until config service is available in proper format
      .then(this.addProvinces)
      .then(this.addCountries)
      .then(this.addColleges)
      .then(this.addLicenses)
      .then(this.addAdvancedPractices)
      .then(this.addJobNames)
      .then(this.addOrganizationNames)
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

  private addColleges(config: Config) {
    return {
      colleges: [
        { code: 1, name: 'College of Physicians and Surgeons of BC (CPSBC)', prefix: '91' },
        { code: 2, name: 'College of Pharmacists of BC (CPBC)', prefix: 'P1' },
        { code: 3, name: 'College of Registered Nurses of BC (CRNBC)', prefix: '96' },
        { code: 4, name: 'None', prefix: null },
      ],
      ...config
    };
  }

  private addLicenses(config: Config) {
    return {
      licenses: [
        { code: 1, name: 'Full - General', collegeCode: 3 },
        { code: 5, name: 'Temporary Registered Nurse', collegeCode: 3 },
        { code: 2, name: 'Full Pharmacist', collegeCode: 1 },
        { code: 3, name: 'Full - Specialty', collegeCode: 1 },
        { code: 4, name: 'Registered Nurse', collegeCode: 2 },
        { code: 5, name: 'Temporary Registered Nurse', collegeCode: 2 },
      ],
      ...config
    };
  }

  private addJobNames(config: Config) {
    return {
      jobNames: [
        { code: 1, name: 'Medical Office Assistant' },
        { code: 2, name: 'Midwife' },
        { code: 3, name: 'Nurse (not Nurse Practitioner)' },
        { code: 4, name: 'Pharmacy Assistant' },
        { code: 5, name: 'Pharmacy Technician' },
        { code: 6, name: 'Registration Clerk' },
        { code: 7, name: 'Ward Clerk' },
        { code: 8, name: 'Other' }
      ],
      ...config
    };
  }

  private addAdvancedPractices(config: Config) {
    return {
      advancedPractices: [
        { code: 1, name: 'Remote Practice' },
        { code: 2, name: 'Reproductive Care' },
        { code: 3, name: 'Sexually Transmitted Infections (STI)' },
        { code: 4, name: 'None' }
      ],
      ...config
    };
  }

  private addOrganizationNames(config: Config) {
    return {
      organizationNames: [
        { code: 1, name: 'Health Authority' },
        { code: 2, name: 'Pharmacy' }
      ],
      ...config
    };
  }
}
