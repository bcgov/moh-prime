import { TestBed } from '@angular/core/testing';

import { EnrolleeUtilsService } from './enrollee-utils.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('EnrolleeUtilsService', () => {
  let service: EnrolleeUtilsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(EnrolleeUtilsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
