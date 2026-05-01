import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ProvisionerAccessResource } from './provisioner-access-resource.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('ProvisionerAccessResource', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}));

  it('should be created', () => {
    const service: ProvisionerAccessResource = TestBed.inject(ProvisionerAccessResource);
    expect(service).toBeTruthy();
  });
});
