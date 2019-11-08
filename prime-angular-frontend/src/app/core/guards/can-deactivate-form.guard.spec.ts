import { TestBed, async, inject } from '@angular/core/testing';

import { CanDeactivateFormGuard } from './can-deactivate-form.guard';

describe('CanDeactivateFormGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CanDeactivateFormGuard]
    });
  });

  it('should create', inject([CanDeactivateFormGuard], (guard: CanDeactivateFormGuard) => {
    expect(guard).toBeTruthy();
  }));
});
