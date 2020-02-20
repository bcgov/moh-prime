import { TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AdjudicationGuard } from './adjudication.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdjudicationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
      ],
      providers: [
        AdjudicationGuard,
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

  it('should create', inject([AdjudicationGuard], (guard: AdjudicationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
