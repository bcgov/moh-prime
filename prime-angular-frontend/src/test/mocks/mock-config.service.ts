import { Injectable, Inject } from '@angular/core';

import { Observable } from 'rxjs';

import { MockConfig } from './mock-config';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration } from '@config/config.model';
import { IConfigService, ConfigService } from '@config/config.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { UtilsService } from '@core/services/utils.service';

@Injectable({
  providedIn: 'root'
})
export class MockConfigService extends ConfigService implements IConfigService {
  constructor(
    protected apiResource: ApiResource,
    protected utilsService: UtilsService
  ) {
    super(apiResource, utilsService);

    // Load the runtime configuration
    this.load().subscribe();
  }

  public load(): Observable<Configuration> {
    return new Observable<Configuration>(subscriber => {
      const configuration = MockConfig.get();

      subscriber.next(this.configuration = configuration);
      subscriber.complete();
    });
  }
}
