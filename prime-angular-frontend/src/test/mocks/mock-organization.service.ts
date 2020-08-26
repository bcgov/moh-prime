import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@shared/models/address.model';
import { Site } from '@registration/shared/models/site.model';
import { IOrganizationService } from '@registration/shared/services/organization.service';
import { Organization } from '@registration/shared/models/organization.model';

export class MockOrganizationService implements IOrganizationService {
  // tslint:disable-next-line: variable-name
  private _organization: BehaviorSubject<Organization>;

  constructor() {
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
    this._organization = new BehaviorSubject<Organization>({
      id: faker.random.number(),
      signingAuthorityId: faker.random.number(),
      signingAuthority: user,
      name: faker.company.companyName(),
      registrationId: faker.random.alphaNumeric(),
      doingBusinessAs: null,
      completed: false,
      acceptedAgreementDate: faker.date.past(2).toDateString(),
      signedAgreementDocuments: [],
      submittedDate: null,
      siteCount: faker.random.number(),
      sites: []
    });
  }

  public get organization$(): BehaviorSubject<Organization> {
    return this._organization;
  }

  public get organization(): Organization {
    return this._organization.value;
  }
}
