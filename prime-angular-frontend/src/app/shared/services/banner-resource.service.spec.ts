import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { BannerResourceService } from './banner-resource.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('BannerResourceService', () => {
  let service: BannerResourceService;

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
    service = TestBed.inject(BannerResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
