import { TestBed, async, inject } from '@angular/core/testing';

import { UnsupportedGuard } from './unsupported.guard';
import { RouterTestingModule } from '@angular/router/testing';

describe('UnsupportedGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        UnsupportedGuard
      ]
    });
  });

  it('should ...', inject([UnsupportedGuard], (guard: UnsupportedGuard) => {
    expect(guard).toBeTruthy();
  }));
});
