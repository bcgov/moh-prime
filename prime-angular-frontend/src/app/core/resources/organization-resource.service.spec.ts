import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { OrganizationResource } from './organization-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrganizationResource', () => {
  let service: OrganizationResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [SharedModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(OrganizationResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
