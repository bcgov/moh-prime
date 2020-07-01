import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { ErrorHandlerInterceptor } from './error-handler.interceptor';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

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
        provide: AuthenticationService,
        useClass: MockAuthenticationService
      }
    ]
  }));

  it('should create', () => {
    const service: ErrorHandlerInterceptor = TestBed.inject(ErrorHandlerInterceptor);
    expect(service).toBeTruthy();
  });
});
