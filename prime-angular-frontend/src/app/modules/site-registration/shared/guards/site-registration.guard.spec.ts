import { TestBed, async, inject } from '@angular/core/testing';

import { SiteRegistrationGuard } from './site-registration.guard';

describe('SiteRegistrationGuardGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SiteRegistrationGuard]
    });
  });

  it('should ...', inject([SiteRegistrationGuard], (guard: SiteRegistrationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
