import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HealthAuthoritySiteFormStateService } from './health-authority-site-form-state.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('HealthAuthoritySiteFormStateService', () => {
  let service: HealthAuthoritySiteFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [ReactiveFormsModule,
        RouterTestingModule,
        MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(HealthAuthoritySiteFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
