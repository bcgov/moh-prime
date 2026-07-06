import { TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AdjudicationGuard } from './adjudication.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AdjudicationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [RouterTestingModule,
        NgxMaterialModule],
    providers: [
        AdjudicationGuard,
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: AuthService,
            useClass: MockAuthService
        },
        {
            provide: PermissionService,
            useClass: MockPermissionService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
  });

  it('should create', inject([AdjudicationGuard], (guard: AdjudicationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
