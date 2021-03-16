import { TestBed } from '@angular/core/testing';
import { FormArray, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { FormUtilsService } from './form-utils.service';

describe('FormUtilsService', () => {
  let service: FormUtilsService;
  const fb: FormBuilder = new FormBuilder();

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule
      ]
    });

    service = TestBed.inject(FormUtilsService);
  });

  it('should create', () => {
    expect(service).toBeTruthy();
  });

  it('should checkValidity', () => {
    const forms = [
      fb.group({
        first: [
          null, [Validators.required]
        ]
      }),
      fb.group({
        second: [
          null
        ]
      })
    ];
    expect(service.checkValidity(forms[0])).toBe(false, 'form should be invalid');
    expect(service.checkValidity(forms[1])).toBe(true, 'form should be valid');

    expect(service.checkValidity(new FormArray(forms))).toBe(false, 'formArray should be invalid');
    expect(service.checkValidity(new FormArray([forms[1]]))).toBe(true, 'formArray should be valid');
  });
});
