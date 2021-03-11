import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { GisEnrolmentFormStateService } from './gis-enrolment-form-state.service';

describe('GisEnrolmentFormStateService', () => {
  let service: GisEnrolmentFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
      ],
    });
    service = TestBed.inject(GisEnrolmentFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
