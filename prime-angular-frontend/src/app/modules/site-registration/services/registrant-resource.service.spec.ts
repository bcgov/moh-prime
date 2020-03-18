import { TestBed } from '@angular/core/testing';

import { RegistrantResource } from './registrant-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('RegistrantResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ],
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should be created', () => {
    const service: RegistrantResource = TestBed.get(RegistrantResource);
    expect(service).toBeTruthy();
  });
});
