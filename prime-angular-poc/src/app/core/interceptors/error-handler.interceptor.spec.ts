import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { ErrorHandlerInterceptor } from './error-handler.interceptor';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ErrorHandlerInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      RouterTestingModule
    ],
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should create', () => {
    const service: ErrorHandlerInterceptor = TestBed.get(ErrorHandlerInterceptor);
    expect(service).toBeTruthy();
  });
});
