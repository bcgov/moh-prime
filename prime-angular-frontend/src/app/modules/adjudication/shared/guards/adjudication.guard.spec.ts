import { TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { AdjudicationGuard } from './adjudication.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

describe('AdjudicationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      providers: [
        AdjudicationGuard,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthenticationService,
          useClass: MockAuthenticationService
        }
      ]
    });
  });

  it('should create', inject([AdjudicationGuard], (guard: AdjudicationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
