import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Address } from '@lib/models/address.model';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { IAuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

export class MockAuthorizedUserService implements IAuthorizedUserService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
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
      phone: faker.phone.phoneNumber(),
      status: AccessStatusEnum.UNDER_REVIEW,
      submittedDate: faker.date.past().toISOString(),
    });
  }

  public get authorizedUser$(): BehaviorSubject<AuthorizedUser> {
    return this._authorizedUser;
  }

  public get authorizedUser(): AuthorizedUser {
    return this._authorizedUser.value;
  }
}
