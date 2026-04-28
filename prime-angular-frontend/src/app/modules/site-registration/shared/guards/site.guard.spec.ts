import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { SiteGuard } from './site.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteGuard', () => {
  let guard: SiteGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [RouterTestingModule,
        MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    guard = TestBed.inject(SiteGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
