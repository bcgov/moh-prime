import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@shared/models/address.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

export class MockSiteService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private readonly _site: BehaviorSubject<Site>;

  constructor() {
    const siteId = faker.random.number();
    const address = new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode());
    const user = {
      id: faker.random.number(),
      userId: `${faker.random.uuid()}`,
      verifiedAddressId: faker.random.number(),
      verifiedAddress: address,
      mailingAddressId: faker.random.number(),
      mailingAddress: address,
      physicalAddressId: faker.random.number(),
      physicalAddress: address,
      hpdid: faker.random.uuid(),
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      givenNames: null,
      preferredFirstName: null,
      preferredMiddleName: null,
      preferredLastName: null,
      dateOfBirth: null,
      jobRoleTitle: null,
      email: null,
      phone: null,
      fax: null,
      smsPhone: null
    };
    this._site = new BehaviorSubject<Site>({
      id: siteId,
      organizationId: faker.random.number(),
      provisionerId: faker.random.number(),
      provisioner: user,
      careSettingCode: faker.random.number(),
      siteVendors: [],
      businessLicence: null,
      doingBusinessAs: faker.company.companyName(),
      physicalAddressId: faker.random.number(),
      physicalAddress: address,
      businessHours: null,
      remoteUsers: [
        {
          id: faker.random.number(),
          firstName: faker.name.firstName(),
          lastName: faker.name.lastName(),
          email: faker.internet.email(),
          remoteUserCertifications: [
            {
              id: faker.random.number(),
              collegeCode: faker.random.number(),
              licenseNumber: faker.random.words(1),
              licenseCode: faker.random.number(),
            }
          ]
        }
      ],
      administratorPharmaNetId: faker.random.number(),
      administratorPharmaNet: user,
      privacyOfficerId: faker.random.number(),
      privacyOfficer: user,
      technicalSupportId: faker.random.number(),
      technicalSupport: user,
      completed: null,
      approvedDate: faker.date.past(2).toDateString(),
      submittedDate: faker.date.past(2).toDateString(),
      adjudicatorId: null,
      adjudicator: null,
      status: SiteStatusType.EDITABLE,
      pec: null,
      flagged: false,
      activeBeforeRegistration: false
    });
  }

  public get site$(): BehaviorSubject<Site> {
    return this._site;
  }

  public get site(): Site {
    return this._site.value;
  }
}
