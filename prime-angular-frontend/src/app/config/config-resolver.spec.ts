import { TestBed, async, inject } from '@angular/core/testing';

import { ConfigResolver } from './config-resolver';

describe('ConfigResolver', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConfigResolver]
    });
  });

  it('should ...', inject([ConfigResolver], (guard: ConfigResolver) => {
    expect(guard).toBeTruthy();
  }));
});
