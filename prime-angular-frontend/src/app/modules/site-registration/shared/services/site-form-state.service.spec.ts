import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteFormStateService } from './site-form-state.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteFormStateService', () => {
  let service: SiteFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [ReactiveFormsModule,
        MatSnackBarModule,
        RouterTestingModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(SiteFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
