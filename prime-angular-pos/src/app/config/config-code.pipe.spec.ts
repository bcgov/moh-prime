import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'mocks/mock-config.service';

import { ConfigCodePipe } from './config-code.pipe';
import { ConfigService } from './config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ConfigCodePipe', () => {
  beforeEach(async(() => {
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

  it('create an instance of Config Code Pipe', inject([ConfigService], (configService: ConfigService) => {
    const pipe = new ConfigCodePipe(configService);
    expect(pipe).toBeTruthy();
  }));

  it('should get college name from a config code', inject([ConfigService], (configService: ConfigService) => {
    const pipe = new ConfigCodePipe(configService);
    const prefix = pipe.transform(configService.colleges[0].code, 'colleges');
    expect(prefix).toBe(configService.colleges[0].name);
  }));

  it('should get a college prefix from a config code', inject([ConfigService], (configService: ConfigService) => {
    const pipe = new ConfigCodePipe(configService);
    const prefix = pipe.transform(configService.colleges[1].code, 'colleges', 'prefix');
    expect(prefix).toBe(configService.colleges[1].prefix);
  }));
});
