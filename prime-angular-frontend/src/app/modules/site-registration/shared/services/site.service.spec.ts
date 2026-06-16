import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteService } from './site.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteService', () => {
  let service: SiteService;

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
    service = TestBed.inject(SiteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
