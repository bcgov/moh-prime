import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed, waitForAsync } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ConfigService } from '@config/config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { CollegeNamePipe } from './college-name.pipe';

/**
 * Based on src\app\config\config-code.pipe.spec.ts
 */
describe('CollegeNamePipe', () => {
  let pipe: CollegeNamePipe;
  let configService: ConfigService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        }
      ]
    });

    configService = TestBed.inject(ConfigService);
    pipe = new CollegeNamePipe(configService);
  }));

  it('create an instance', () => expect(pipe).toBeTruthy());
});
