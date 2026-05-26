import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { OrgBookResource } from './org-book-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { UtilsService } from '@core/services/utils.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrgBookResource', () => {
  let service: OrgBookResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        UtilsService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(OrgBookResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
