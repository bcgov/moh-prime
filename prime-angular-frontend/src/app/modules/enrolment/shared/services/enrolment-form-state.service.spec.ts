import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolmentFormStateService } from './enrolment-form-state.service';

describe('EnrolmentFormStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      ReactiveFormsModule,
      RouterTestingModule,
    ]
  }));

  it('should create', () => {
    const service: EnrolmentFormStateService = TestBed.inject(EnrolmentFormStateService);
    expect(service).toBeTruthy();
  });
});
