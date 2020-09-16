import { TestBed } from '@angular/core/testing';

import { OrganizationFormStateService } from './organization-form-state.service';
import { ReactiveFormsModule } from '@angular/forms';

describe('OrganizationFormStateService', () => {
  let service: OrganizationFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule
      ]
    });
    service = TestBed.inject(OrganizationFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
