import { TestBed, async, inject } from '@angular/core/testing';

import { UnsupportedGuard } from './unsupported.guard';

describe('UnsupportedGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        UnsupportedGuard
      ]
    });
  });

  it('should ...', inject([UnsupportedGuard], (guard: UnsupportedGuard) => {
    expect(guard).toBeTruthy();
  }));
});
