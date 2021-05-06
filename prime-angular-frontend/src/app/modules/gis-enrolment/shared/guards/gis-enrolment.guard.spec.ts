import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { GisEnrolmentGuard } from './gis-enrolment.guard';

describe('GisEnrolmentGuard', () => {
  let guard: GisEnrolmentGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      providers: [
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
    guard = TestBed.inject(GisEnrolmentGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
