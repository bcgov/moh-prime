import { TestBed } from '@angular/core/testing';

import { SiteGuard } from './site.guard';

describe('SiteGuard', () => {
  let guard: SiteGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SiteGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
