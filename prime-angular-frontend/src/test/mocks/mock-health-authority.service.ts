import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';
import { HealthAuthority } from '@shared/models/health-authority.model';

export class MockHealthAuthorityService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  private _healthAuthority: BehaviorSubject<HealthAuthority>;

  constructor() {
    this._healthAuthority = new BehaviorSubject<HealthAuthority>({
      id: faker.random.number(),
      name: faker.random.words(2),
      careTypes: [],
      vendors: [],
      privacyOffice: null,
      technicalSupports: [],
      pharmanetAdministrators: []
    });
  }

  public get healthAuthority$(): BehaviorSubject<HealthAuthority> {
    return this._healthAuthority;
  }

  public get healthAuthority(): HealthAuthority {
    return this._healthAuthority.value;
  }
}
