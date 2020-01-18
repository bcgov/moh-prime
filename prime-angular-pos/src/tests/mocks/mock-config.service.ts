import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { Configuration } from '@config/config.model';
import { IConfigService, ConfigService } from '@config/config.service';

export class MockConfigService extends ConfigService implements IConfigService {
  constructor(
    @Inject(APP_CONFIG) protected appConfig: AppConfig,
    protected http: HttpClient
  ) {
    super(appConfig, http);

    // Load the runtime configuration
    this.load().subscribe();
  }

  public load(): Observable<Configuration> {
    return new Observable<Configuration>(subscriber => {
      const configuration = {
        config: []
      };

      subscriber.next(this.configuration = configuration);
      subscriber.complete();
    });
  }
}
