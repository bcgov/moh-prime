import { TestBed } from '@angular/core/testing';

import { HealthAuthorityResolver } from './health-authority.resolver';

describe('HealthAuthorityResolver', () => {
  let resolver: HealthAuthorityResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(HealthAuthorityResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
