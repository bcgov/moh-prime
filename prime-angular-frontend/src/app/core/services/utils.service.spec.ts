import { TestBed } from '@angular/core/testing';

import { UtilsService } from './utils.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('UtilsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should create', () => {
    const service: UtilsService = TestBed.inject(UtilsService);
    expect(service).toBeTruthy();
  });
});
