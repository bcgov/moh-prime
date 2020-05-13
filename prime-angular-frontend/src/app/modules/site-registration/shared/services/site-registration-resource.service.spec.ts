import { TestBed } from '@angular/core/testing';

import { SiteRegistrationResource } from './site-registration-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SharedModule } from '@shared/shared.module';

describe('SiteRegistrationResource', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      SharedModule
    ],
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should be created', () => {
    const service: SiteRegistrationResource = TestBed.inject(SiteRegistrationResource);
    expect(service).toBeTruthy();
  });
});
