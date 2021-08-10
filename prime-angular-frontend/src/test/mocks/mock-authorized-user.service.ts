import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@shared/models/address.model';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { IAuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

export class MockAuthorizedUserService implements IAuthorizedUserService {
  // tslint:disable-next-line: variable-name
  private readonly _authorizedUser: BehaviorSubject<AuthorizedUser>;

  constructor() {
    const address = new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode());
    this._authorizedUser = new BehaviorSubject<AuthorizedUser>({
      userId: `${faker.random.uuid()}`,
      hpdid: `${faker.random.uuid()}`,
      firstName: faker.name.firstName(),
      lastName: faker.name.lastName(),
      givenNames: faker.name.firstName(),
      dateOfBirth: faker.date.past().toISOString(),
      verifiedAddress: address,
      physicalAddress: address,
      email: faker.internet.email(),
      jobRoleTitle: '',
      healthAuthorityCode: HealthAuthorityEnum.FRASER_HEALTH,
      employmentIdentifier: '',
      phone: faker.phone.phoneNumber(),
      status: AccessStatusEnum.UNDER_REVIEW,
    });
  }

  public get authorizedUser$(): BehaviorSubject<AuthorizedUser> {
    return this._authorizedUser;
  }

  public get authorizedUser(): AuthorizedUser {
    return this._authorizedUser.value;
  }
}
