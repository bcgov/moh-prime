import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { ConfigCodePipe } from './config-code.pipe';
import { ConfigService } from './config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ConfigCodePipe', () => {
  let pipe: ConfigCodePipe;
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
    pipe = new ConfigCodePipe(configService);
  }));

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should get a config name from a config code', () => {
    const country = configService.countries[0];
    const result = pipe.transform(country.code, 'countries');
    expect(result).toBe(country.name);
  });

  it('should not fail when passed a null', () => {
    const country = configService.countries[0];
    const result = pipe.transform(null, 'countries');
    expect(result).toBe('');
  });
});
