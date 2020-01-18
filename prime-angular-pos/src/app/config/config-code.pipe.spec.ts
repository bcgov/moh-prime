import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'tests/mocks/mock-config.service';

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
});
