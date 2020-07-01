import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@shared/models/address.model';
import { Site } from '@registration/shared/models/site.model';
import { ISiteRegistrationService } from '@registration/shared/services/site-registration.service';

export class MockSiteRegistrationService implements ISiteRegistrationService {
  // tslint:disable-next-line: variable-name
  private _site: BehaviorSubject<Site>;

  constructor() {
    const siteId = faker.random.number();
    const address = new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode());
    const user = {
      id: faker.random.number(),
      userId: `${faker.random.uuid()}`,
      addressId: faker.random.number(),
      physicalAddressId: faker.random.number(),
      physicalAddress: address,
      hpdid: faker.random.uuid(),
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      dateOfBirth: null,
      jobRoleTitle: null,
      email: null,
      phone: null
    };
    this._site = new BehaviorSubject<Site>({
      id: siteId,
      locationId: faker.random.number(),
      location: {
        id: faker.random.number(),
        name: faker.company.companyName(),
        doingBusinessAs: null,
        administratorPharmaNetId: faker.random.number(),
        administratorPharmaNet: user,
        privacyOfficerId: faker.random.number(),
        privacyOfficer: user,
        technicalSupportId: faker.random.number(),
        technicalSupport: user,
        organizationId: faker.random.number(),
        organization: {
          id: faker.random.number(),
          signingAuthorityId: faker.random.number(),
          signingAuthority: user,
          name: faker.company.companyName(),
          registrationId: faker.random.number(),
          doingBusinessAs: null,
          organizationTypeCode: null,
          acceptedAgreementDate: faker.date.past(2).toDateString(),
          locations: [],
          submittedDate: faker.date.past(2).toDateString(),
          completed: true,
          signedAgreements: []
        },
        physicalAddressId: faker.random.number(),
        physicalAddress: address,
        businessHours: null,
        sites: []
      },
      vendorCode: null,
      vendor: null,
      remoteUsers: [],
      businessLicences: [],
      provisionerId: faker.random.number(),
      provisioner: user,
      pec: null,
      completed: null,
      approvedDate: faker.date.past(2).toDateString(),
      submittedDate: faker.date.past(2).toDateString()
    });
  }

  public get site$(): BehaviorSubject<Site> {
    return this._site;
  }

  public get site(): Site {
    return this._site.value;
  }
}
