import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EmailTemplateResourceService } from './email-template-resource.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EmailTemplateResourceService', () => {
  let service: EmailTemplateResourceService;

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
    service = TestBed.inject(EmailTemplateResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
