import { TestBed, inject } from '@angular/core/testing';

import { ElectronicAgreementGuard } from './electronic-agreement.guard';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SharedModule } from '@shared/shared.module';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

describe('ElectronicAgreementGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          {
            path: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
            component: ElectronicAgreementGuard
          }
        ]),
        HttpClientTestingModule,
        SharedModule
      ],
      providers: [
        ElectronicAgreementGuard,
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

  it('should ...', inject([ElectronicAgreementGuard], (guard: ElectronicAgreementGuard) => {
    expect(guard).toBeTruthy();
  }));
});
