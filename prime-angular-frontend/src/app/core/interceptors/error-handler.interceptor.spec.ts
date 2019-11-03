import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { ErrorHandlerInterceptor } from './error-handler.interceptor';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

describe('ErrorHandlerInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      RouterTestingModule
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
  }));

  it('should be created', () => {
    const service: ErrorHandlerInterceptor = TestBed.get(ErrorHandlerInterceptor);
    expect(service).toBeTruthy();
  });
});
