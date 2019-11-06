import { TestBed, async, inject } from '@angular/core/testing';

import { BaseGuard } from './base.guard';

describe('BaseGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BaseGuard]
    });
  });

  it('should ...', inject([BaseGuard], (guard: BaseGuard) => {
    expect(guard).toBeTruthy();
  }));
});
