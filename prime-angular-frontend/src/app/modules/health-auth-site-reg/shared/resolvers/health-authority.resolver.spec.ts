import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthorityResolver } from './health-authority.resolver';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('HealthAuthorityResolver', () => {
  let resolver: HealthAuthorityResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: AppConfig
        },
        CapitalizePipe,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    resolver = TestBed.inject(HealthAuthorityResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
