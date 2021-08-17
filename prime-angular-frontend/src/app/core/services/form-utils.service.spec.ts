import { TestBed } from '@angular/core/testing';
import { FormArray, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { Person } from '@lib/models/person.model';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Address } from '@shared/models/address.model';
import { FormUtilsService } from './form-utils.service';

describe('FormUtilsService', () => {
  let service: FormUtilsService;
  const fb: FormBuilder = new FormBuilder();

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
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

  it('should toPersonFormModel', () => {
    const physicalAddress: Address = {
      city: 'Victoria',
      countryCode: 'CA',
      postal: 'V1X4M6',
      provinceCode: 'BC',
      street: '123 Main ST',
      id: 1,
      street2: '#123',
    };
    const mailingAddress: Address = {
      city: 'Vancouver',
      countryCode: 'CA',
      postal: 'V5X4X6',
      provinceCode: 'BC',
      street: '456 Main ST',
      id: 2,
      street2: '#456',
    };
    const data: Person = {
      id: 1,
      email: 'test@test.com',
      firstName: 'first',
      lastName: 'last',
      jobRoleTitle: 'manager',
      phone: '5555555555',
      fax: '6666666666',
      smsPhone: '4444444444',
      mailingAddressId: 1,
      physicalAddressId: 1,
      physicalAddress,
      mailingAddress
    };

    let formGroup = fb.group({});
    service.toPersonFormModel<Person>([formGroup, data]);
    expect(formGroup.getRawValue()).toEqual({});

    formGroup = fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      fax: [
        null,
        [FormControlValidators.phone]
      ],
      smsPhone: [
        null,
        [FormControlValidators.phone]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      physicalAddress: service.buildAddressForm({
        exclude: ['street2']
      })
    });

    service.toPersonFormModel<Person>([formGroup, data]);
    expect(formGroup.getRawValue().id).toEqual(data.id);
    expect(formGroup.getRawValue().firstName).toEqual(data.firstName);
    expect(formGroup.getRawValue().lastName).toEqual(data.lastName);
    expect(formGroup.getRawValue().jobRoleTitle).toEqual(data.jobRoleTitle);
    expect(formGroup.getRawValue().phone).toEqual(data.phone);
    expect(formGroup.getRawValue().fax).toEqual(data.fax);
    expect(formGroup.getRawValue().smsPhone).toEqual(data.smsPhone);
    expect(formGroup.getRawValue().email).toEqual(data.email);
    expect(formGroup.getRawValue().physicalAddress).toEqual(data.physicalAddress);
    expect(formGroup.getRawValue().mailingAddress).toBeUndefined();

    formGroup.addControl('mailingAddress', service.buildAddressForm({
      exclude: ['street2']
    }));

    service.toPersonFormModel<Person>([formGroup, data]);
    expect(formGroup.getRawValue().mailingAddress).toEqual(data.mailingAddress);
  });
});
