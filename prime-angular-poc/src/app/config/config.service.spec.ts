import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { ConfigService } from './config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ConfigService', () => {
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

  it('should create', () => {
    const service: ConfigService = TestBed.get(ConfigService);
    expect(service).toBeTruthy();
  });
});
