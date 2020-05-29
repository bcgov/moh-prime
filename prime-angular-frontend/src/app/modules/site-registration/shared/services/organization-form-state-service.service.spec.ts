import { TestBed } from '@angular/core/testing';

import { OrganizationFormStateServiceService } from './organization-form-state-service.service';

describe('OrganizationFormStateServiceService', () => {
  let service: OrganizationFormStateServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrganizationFormStateServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
