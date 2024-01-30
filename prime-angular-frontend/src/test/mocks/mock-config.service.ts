import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { MockConfig } from './mock-config';

import { Configuration } from '@config/config.model';
import { IConfigService, ConfigService } from '@config/config.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';

@Injectable({
  providedIn: 'root'
})
export class MockConfigService extends ConfigService implements IConfigService {
  constructor(
    protected apiResource: ApiResource,
    protected utilsService: UtilsService,
    protected apiResourceUtilsService: ApiResourceUtilsService
  ) {
    super(apiResource, utilsService, apiResourceUtilsService);

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
