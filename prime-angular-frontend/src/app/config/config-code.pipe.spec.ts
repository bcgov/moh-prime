import { TestBed, async, inject } from '@angular/core/testing';

import { ConfigCodePipe } from './config-code.pipe';
import { ConfigService } from './config.service';

class MockConfigService extends ConfigService {
  public get colleges() {
    return [
      {
        prefix: 'C0',
        code: 0,
        name: 'College of Registered Nurses',
        collegeLicenses: [],
        collegePractices: []
      },
      {
        prefix: 'C1',
        code: 1,
        name: 'College of Doctors',
        collegeLicenses: [],
        collegePractices: []
      }
    ];
  }
}

describe('ConfigCodePipe', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: ConfigService,
          useClass: MockConfigService
        }
      ]
    });
  }));

  it('create an instance of Config Code Pipe', () => {
    inject([MockConfigService], (config: MockConfigService) => {
      const pipe = new ConfigCodePipe(config);
      expect(pipe).toBeTruthy();
    });
  });

  it('should get college name from a config code', () => {
    inject([MockConfigService], (config: MockConfigService) => {
      const pipe = new ConfigCodePipe(config);
      const prefix = pipe.transform(config.colleges[0].code, 'colleges');
      expect(prefix).toBe(config.colleges[0].name);
    });
  });

  it('should get a college prefix from a config code', () => {
    inject([MockConfigService], (config: MockConfigService) => {
      const pipe = new ConfigCodePipe(config);
      const prefix = pipe.transform(config.colleges[1].code, 'colleges', 'prefix');
      expect(prefix).toBe(config.colleges[1].prefix);
    });
  });
});
