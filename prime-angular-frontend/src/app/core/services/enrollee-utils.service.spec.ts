import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolleeUtilsService } from './enrollee-utils.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

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
