import { TestBed } from '@angular/core/testing';

import { OrganizationFormStateService } from './organization-form-state.service';
import { SharedModule } from '@shared/shared.module';

describe('OrganizationFormStateService', () => {
  let service: OrganizationFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        SharedModule
      ]
    });
    service = TestBed.inject(OrganizationFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
