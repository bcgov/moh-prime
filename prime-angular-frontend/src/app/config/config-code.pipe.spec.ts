import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { ConfigCodePipe } from './config-code.pipe';
import { ConfigService } from './config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ConfigCodePipe', () => {
  let pipe: ConfigCodePipe;
  beforeEach(inject([ConfigService], (configService: ConfigService) => pipe = new ConfigCodePipe(configService)));

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
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
  }));

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should get a config name from a config code', inject([ConfigService], (configService: ConfigService) => {
    const country = configService.countries[0];
    const result = pipe.transform(country.code, 'countries');
    expect(result).toBe(country.name);
  }));

  it('should not fail when passed a null', inject([ConfigService], (configService: ConfigService) => {
    const country = configService.countries[0];
    const result = pipe.transform(null, 'countries');
    expect(result).toBe(country.name);
  }));
});
