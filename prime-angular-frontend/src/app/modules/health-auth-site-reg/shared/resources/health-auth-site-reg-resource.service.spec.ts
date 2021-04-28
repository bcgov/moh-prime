import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { HealthAuthSiteRegResource } from './health-auth-site-reg-resource.service';

describe('HealthAuthSiteRegResourceService', () => {
  let service: HealthAuthSiteRegResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SharedModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    });
    service = TestBed.inject(HealthAuthSiteRegResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
