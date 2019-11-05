import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { EnrolmentStateService } from './enrolment-state.service';

describe('EnrolmentStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      ReactiveFormsModule
    ]
  }));

  it('should be created', () => {
    const service: EnrolmentStateService = TestBed.get(EnrolmentStateService);
    expect(service).toBeTruthy();
  });
});
