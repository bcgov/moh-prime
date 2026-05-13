import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed, waitForAsync } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ConfigService } from '@config/config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { HealthAuthorityVendorPipe } from './health-authority-vendor.pipe';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

/**
 * Based on src\app\config\config-code.pipe.spec.ts
 */
describe('HealthAuthorityVendorPipe', () => {
  let pipe: HealthAuthorityVendorPipe;
  let configService: ConfigService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    imports: [MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});

    configService = TestBed.inject(ConfigService);
    pipe = new HealthAuthorityVendorPipe(configService);
  }));

  it('create an instance', () => expect(pipe).toBeTruthy());
});
