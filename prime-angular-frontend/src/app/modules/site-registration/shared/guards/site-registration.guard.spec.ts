import { TestBed, async, inject } from '@angular/core/testing';

import { SiteRegistrationGuard } from './site-registration.guard';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('SiteRegistrationGuardGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [
        SiteRegistrationGuard,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        }
      ]
    });
  });

  it('should ...', inject([SiteRegistrationGuard], (guard: SiteRegistrationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
