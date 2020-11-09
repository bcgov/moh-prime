import { TestBed } from '@angular/core/testing';

import { UnderagedGuard } from './underaged.guard';

describe('UnderagedGuard', () => {
  let guard: UnderagedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UnderagedGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
