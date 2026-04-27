import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { PaperEnrolmentGuard } from './paper-enrolment.guard';
import { KeycloakService } from 'keycloak-angular';
import { APP_CONFIG, APP_DI_CONFIG } from '../../../../app-config.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('PaperEnrolmentGuard', () => {
  let guard: PaperEnrolmentGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [RouterTestingModule,
        MatSnackBarModule],
    providers: [
        PaperEnrolmentGuard,
        KeycloakService,
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    guard = TestBed.inject(PaperEnrolmentGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
