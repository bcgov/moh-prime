import { TestBed } from '@angular/core/testing';

import { OrganizationGuard } from './organization.guard';

describe('OrganizationGuard', () => {
  let guard: OrganizationGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(OrganizationGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
