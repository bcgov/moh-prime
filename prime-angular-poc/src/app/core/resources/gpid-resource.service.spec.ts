import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { GpidResource } from './gpid-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('GpidResource', () => {
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
    const service: GpidResource = TestBed.get(GpidResource);
    expect(service).toBeTruthy();
  });
});
