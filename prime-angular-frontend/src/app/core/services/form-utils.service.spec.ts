import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { FormUtilsService } from './form-utils.service';

describe('FormUtilsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule
      ]
    });
  });

  it('should create', () => {
    const service: FormUtilsService = TestBed.inject(FormUtilsService);
    expect(service).toBeTruthy();
  });
});
