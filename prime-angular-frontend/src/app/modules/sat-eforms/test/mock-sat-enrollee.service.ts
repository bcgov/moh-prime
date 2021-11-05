import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@lib/models/address.model';

import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { ISatEnrolleeService } from '@sat/shared/services/sat-enrollee.service';

export class MockSatEnrolleeService implements ISatEnrolleeService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _enrollee: BehaviorSubject<SatEnrollee>;

  constructor() {
    const enrolmentId = faker.random.number();
    this._enrollee = new BehaviorSubject<SatEnrollee>({
      id: enrolmentId,
      hpdid: null,
      userId: faker.random.uuid(),
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      givenNames: faker.name.firstName(),
      dateOfBirth: faker.date.past(2).toDateString(),
      preferredFirstName: null,
      preferredMiddleName: null,
      preferredLastName: null,
      email: faker.internet.email(),
      phone: faker.phone.phoneNumber(),
      physicalAddress: new Address(),
      verifiedAddress: new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode()),
      partyCertifications: [],
      submittedDate: faker.date.past(1).toDateString()
    });
  }

  public set enrollee(enrollee: SatEnrollee) {
    this._enrollee.next(enrollee);
  }

  public get enrollee(): SatEnrollee {
    return this._enrollee.value;
  }
}
