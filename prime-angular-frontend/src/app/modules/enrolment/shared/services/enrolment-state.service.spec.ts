import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolmentStateService } from './enrolment-state.service';

describe('EnrolmentStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      ReactiveFormsModule,
      RouterTestingModule,
    ]
  }));

  it('should create', () => {
    const service: EnrolmentStateService = TestBed.get(EnrolmentStateService);
    expect(service).toBeTruthy();
  });
});
