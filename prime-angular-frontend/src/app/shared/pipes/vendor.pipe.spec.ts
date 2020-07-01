import { TestBed, async, inject } from '@angular/core/testing';

import { ConfigService } from '@config/config.service';

import { VendorPipe } from './vendor.pipe';

describe('VendorPipe', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ConfigService
      ]
    });
  }));

  it('create an instance', inject([ConfigService], (config: ConfigService) => {
    const pipe = new VendorPipe(config);
    expect(pipe).toBeTruthy();
  }));
});
