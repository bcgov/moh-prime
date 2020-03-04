import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration } from '@config/config.model';
import { IConfigService, ConfigService } from '@config/config.service';

export class MockConfigService extends ConfigService implements IConfigService {
  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig,
    protected http: HttpClient
  ) {
    super(config, http);

    // Load the runtime configuration
    this.load().subscribe();
  }

  public load(): Observable<Configuration> {
    return new Observable<Configuration>(subscriber => {
      const configuration = {
        countries: [
          { code: 'CA', name: 'Canada' },
          { code: 'US', name: 'United States' }
        ],
        colleges: [
          {
            code: 1,
            name: 'College of Physicians and Surgeons of BC (CPSBC)',
            prefix: '91',
            collegeLicenses: [
              { collegeCode: 1, licenseCode: 3 },
              { collegeCode: 1, licenseCode: 2 }
            ],
            collegePractices: [
              { collegeCode: 1, practiceCode: 1 },
              { collegeCode: 1, practiceCode: 2 },
              { collegeCode: 1, practiceCode: 3 },
              { collegeCode: 1, practiceCode: 4 }
            ]
          },
          {
            code: 2,
            name: 'College of Pharmacists of BC (CPBC)',
            prefix: 'P1',
            collegeLicenses: [
              { collegeCode: 2, licenseCode: 5 },
              { collegeCode: 2, licenseCode: 4 }
            ],
            collegePractices: [
              { collegeCode: 2, practiceCode: 2 },
              { collegeCode: 2, practiceCode: 1 },
              { collegeCode: 2, practiceCode: 4 },
              { collegeCode: 2, practiceCode: 3 }
            ]
          },
          {
            code: 3,
            name: 'College of Registered Nurses of BC (CRNBC)',
            prefix: '96',
            collegeLicenses: [
              { collegeCode: 3, licenseCode: 5 },
              { collegeCode: 3, licenseCode: 1 }
            ],
            collegePractices: [
              { collegeCode: 3, practiceCode: 4 },
              { collegeCode: 3, practiceCode: 1 },
              { collegeCode: 3, practiceCode: 2 },
              { collegeCode: 3, practiceCode: 3 }
            ]
          },
          {
            code: 4,
            name: 'None',
            prefix: null,
            collegeLicenses: [],
            collegePractices: []
          }
        ],
        jobNames: [
          { code: 1, name: 'Medical Office Assistant' },
          { code: 2, name: 'Midwife' },
          { code: 3, name: 'Nurse (not nurse practitioner)' },
          { code: 4, name: 'Pharmacy Assistant' },
          { code: 5, name: 'Pharmacy Technician' },
          { code: 6, name: 'Registration Clerk' },
          { code: 7, name: 'Ward Clerk' },
          { code: 8, name: 'Other' }
        ],
        licenses: [
          {
            code: 1,
            name: 'Full - General',
            weight: 1,
            collegeLicenses: [
              { collegeCode: 3, licenseCode: 1 }
            ]
          },
          {
            code: 2,
            name: 'Full - Pharmacist',
            weight: 2,
            collegeLicenses: [
              { collegeCode: 1, licenseCode: 2 }
            ]
          },
          {
            code: 3,
            name: 'Full - Specialty',
            weight: 3,
            collegeLicenses: [
              { collegeCode: 1, licenseCode: 3 }
            ]
          },
          {
            code: 4,
            name: 'Registered Nurse',
            weight: 4,
            collegeLicenses: [
              { collegeCode: 2, licenseCode: 4 }
            ]
          },
          {
            code: 5,
            name: 'Temporary Registered Nurse',
            weight: 5,
            collegeLicenses: [
              { collegeCode: 2, licenseCode: 5 },
              { collegeCode: 3, licenseCode: 5 }
            ]
          }
        ],
        organizationTypes: [
          { code: 1, name: 'Health Authority' },
          { code: 2, name: 'Pharmacy' }
        ],
        practices: [
          {
            code: 1,
            name: 'Remote Practice',
            collegePractices: [
              { collegeCode: 1, practiceCode: 1 },
              { collegeCode: 2, practiceCode: 1 },
              { collegeCode: 3, practiceCode: 1 }
            ]
          },
          {
            code: 2,
            name: 'Reproductive Care',
            collegePractices: [
              { collegeCode: 1, practiceCode: 2 },
              { collegeCode: 2, practiceCode: 2 },
              { collegeCode: 3, practiceCode: 2 }
            ]
          },
          {
            code: 3,
            name: 'Sexually Transmitted Infections (STI)',
            collegePractices: [
              { collegeCode: 1, practiceCode: 3 },
              { collegeCode: 2, practiceCode: 3 },
              { collegeCode: 3, practiceCode: 3 }
            ]
          },
          {
            code: 4,
            name: 'None',
            collegePractices: [
              { collegeCode: 1, practiceCode: 4 },
              { collegeCode: 2, practiceCode: 4 },
              { collegeCode: 3, practiceCode: 4 }
            ]
          }
        ],
        provinces: [
          { code: 'AB', name: 'Alberta', countryCode: 'CA' },
          { code: 'BC', name: 'British Columbia', countryCode: 'CA' },
          { code: 'MB', name: 'Manitoba', countryCode: 'CA' },
          { code: 'NB', name: 'New Brunswick', countryCode: 'CA' },
          { code: 'NL', name: 'Newfoundland and Labrador', countryCode: 'CA' },
          { code: 'NS', name: 'Nova Scotia', countryCode: 'CA' },
          { code: 'NT', name: 'Northwest Territories', countryCode: 'CA' },
          { code: 'NU', name: 'Nunavut', countryCode: 'CA' },
          { code: 'ON', name: 'Ontario', countryCode: 'CA' },
          { code: 'PE', name: 'Prince Edward Island', countryCode: 'CA' },
          { code: 'QC', name: 'Quebec', countryCode: 'CA' },
          { code: 'SK', name: 'Saskatchewan', countryCode: 'CA' },
          { code: 'YT', name: 'Yukon', countryCode: 'CA' }
        ],
        statuses: [
          { code: 1, name: 'Active' },
          { code: 4, name: 'Locked' },
          { code: 3, name: 'Requires TOA' },
          { code: 2, name: 'Under Review' }
        ],
        statusReasons: [
          { code: 7, name: 'Self Declaration' },
          { code: 1, name: 'Automatic' },
          { code: 2, name: 'Manual' },
          { code: 3, name: 'Name Discrepancy' },
          { code: 4, name: 'Not in PharmaNet' },
          { code: 5, name: 'Insulin Pump Provider' },
          { code: 6, name: 'Licence Class' },
          { code: 8, name: 'Contact address or Identity Address Out of British Columbia' }
        ],
        privilegeGroups: [
          { code: 1, privilegeTypeCode: 2, name: 'Submit and Access Claims' },
          { code: 2, privilegeTypeCode: 2, name: 'Record Medical History' },
          { code: 3, privilegeTypeCode: 2, name: 'Access Medical History' },
          { code: 4, privilegeTypeCode: 1, name: 'Role' },
          { code: 5, privilegeTypeCode: 1, name: 'RU That Can Have OBO\'s' }
        ],
        privilegeTypes: [
          { code: 1, name: 'Allowable Role' },
          { code: 2, name: 'Allowable Transaction' }
        ]
      };

      subscriber.next(this.configuration = configuration);
      subscriber.complete();
    });
  }
}
