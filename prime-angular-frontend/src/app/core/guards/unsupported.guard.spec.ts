import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { UnsupportedGuard } from './unsupported.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('UnsupportedGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        UnsupportedGuard
      ]
    });
  });

  it('should ...', inject([UnsupportedGuard], (guard: UnsupportedGuard) => {
    expect(guard).toBeTruthy();
  }));
});
