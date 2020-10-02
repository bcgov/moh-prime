import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { OrganizationFormStateService } from './organization-form-state.service';

describe('OrganizationFormStateService', () => {
  let service: OrganizationFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule
      ]
    });
    service = TestBed.inject(OrganizationFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
