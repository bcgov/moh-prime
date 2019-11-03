import { Observable } from 'rxjs';

import { Configuration } from '@config/config.model';
import { IConfigService, ConfigService } from '@config/config.service';

export class MockConfigService extends ConfigService implements IConfigService {
  public load(): Observable<Configuration> {
    return new Observable<Configuration>(subscriber => {
      const configuration = {
        countries: [
          { code: 'CA', name: 'Canada' }
        ],
        colleges: [],
        jobNames: [],
        licenses: [],
        organizationNames: [
          { code: 1, name: 'Shoppers Drug Mart' },
          { code: 2, name: 'London Drugs' }
        ],
        organizationTypes: [],
        practices: [],
        provinces: [
          { code: 'AB', name: 'Alberta' },
          { code: 'BC', name: 'British Columbia' }
        ],
        statuses: []
      };

      subscriber.next(this.configuration = configuration);
      subscriber.complete();
    });
  }
}
