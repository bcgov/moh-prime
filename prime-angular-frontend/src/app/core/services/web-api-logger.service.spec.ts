import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { WebApiLoggerService } from './web-api-logger.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('WebApiLoggerService', () => {
  let service: WebApiLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(WebApiLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
