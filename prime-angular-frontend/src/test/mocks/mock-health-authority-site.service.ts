import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@lib/models/address.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export class MockHealthAuthoritySiteService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private readonly _site: BehaviorSubject<HealthAuthoritySite>;

  constructor() {
    const siteId = faker.random.number();
    const address = new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode());
    const healthAuthoritySite = HealthAuthoritySite.toHealthAuthoritySite({
      id: siteId,
      healthAuthorityOrganizationId: HealthAuthorityEnum.INTERIOR_HEALTH,
      healthAuthorityVendor: {
        id: faker.random.number(),
        healthAuthorityOrganizationId: faker.random.number(),
        vendorCode: faker.random.number()
      },
      healthAuthorityCareType: {
        id: faker.random.number(),
        healthAuthorityOrganizationId: faker.random.number(),
        careType: faker.random.words(1)
      },
      siteName: faker.random.words(2),
      pec: faker.random.number().toString(),
      securityGroupCode: faker.random.number(),
      physicalAddress: address,
      businessHours: null,
      healthAuthorityPharmanetAdministratorId: faker.random.number(),
      healthAuthorityTechnicalSupportId: faker.random.number(),
      completed: true,
      submittedDate: faker.date.past(2).toDateString(),
      approvedDate: faker.date.past(2).toDateString(),
      status: SiteStatusType.EDITABLE
    });
    this._site = new BehaviorSubject<HealthAuthoritySite>(healthAuthoritySite);
  }

  public get site$(): BehaviorSubject<HealthAuthoritySite> {
    return this._site;
  }

  public get site(): HealthAuthoritySite {
    return this._site.value;
  }
}
