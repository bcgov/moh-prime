import { TestBed } from '@angular/core/testing';

import { OrganizationFormStateService } from './organization-form-state-service';

describe('OrganizationFormStateService', () => {
  let service: OrganizationFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrganizationFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
