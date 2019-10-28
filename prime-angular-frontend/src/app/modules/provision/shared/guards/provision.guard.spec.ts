import { TestBed, async, inject } from '@angular/core/testing';

import { ProvisionGuard } from './provision.guard';

describe('ProvisionGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProvisionGuard]
    });
  });

  it('should ...', inject([ProvisionGuard], (guard: ProvisionGuard) => {
    expect(guard).toBeTruthy();
  }));
});
