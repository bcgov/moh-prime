import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@shared/models/address.model';
import { Site } from '@registration/shared/models/site.model';
import { ISiteService } from '@registration/shared/services/site.service';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

export class MockSiteService implements ISiteService {
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
      mailingAddressId: faker.random.number(),
      mailingAddress: address,
      hpdid: faker.random.uuid(),
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
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
      businessLicenceDocuments: [],
      businessLicenceGuid: faker.random.uuid(),
      deferredLicenceReason: faker.random.words(),
      doingBusinessAs: faker.company.companyName(),
      physicalAddressId: faker.random.number(),
      physicalAddress: address,
      businessHours: null,
      remoteUsers: null,
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
      status: SiteStatusType.APPROVED,
      pec: null
    });
  }

  public get site$(): BehaviorSubject<Site> {
    return this._site;
  }

  public get site(): Site {
    return this._site.value;
  }
}
