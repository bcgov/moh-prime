import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { SharedModule } from '@shared/shared.module';

import { HealthAuthorityResource } from './health-authority-resource.service';

describe('HealthAuthorityResourceService', () => {
  let service: HealthAuthorityResource;

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
      ]
    });
    service = TestBed.inject(HealthAuthorityResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
