import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { PrivacyOffice } from '@lib/models/privacy-office.model';

export class PrivacyOfficePageFormState extends AbstractFormState<PrivacyOffice> {
  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get email(): UntypedFormControl {
    return this.formInstance.get('email') as UntypedFormControl;
  }

  public get phone(): UntypedFormControl {
    return this.formInstance.get('phone') as UntypedFormControl;
  }

  public get phoneExtension(): UntypedFormControl {
    return this.formInstance.get('phoneExtension') as UntypedFormControl;
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.formInstance.get('physicalAddress') as UntypedFormGroup;
  }

  public get privacyOfficer(): UntypedFormGroup {
    return this.formInstance.get('privacyOfficer') as UntypedFormGroup;
  }

  public get json(): PrivacyOffice {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.value;
  }

  public patchValue(model: PrivacyOffice): void {
    if (!model) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(disabled: boolean = false): void {
    this.formInstance = this.fb.group({
      id: [
        0,
        []
      ],
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2']
      }),
      privacyOfficer: this.fb.group({
        id: [0, []],
        firstName: [
          { value: null, disabled },
          [Validators.required]
        ],
        lastName: [
          { value: null, disabled },
          [Validators.required]
        ],
        phone: [
          null,
          [Validators.required, FormControlValidators.phone]
        ],
        phoneExtension: [
          null,
          []
        ],
        smsPhone: [
          null,
          [FormControlValidators.phone]
        ],
        email: [
          null,
          [Validators.required, FormControlValidators.email]
        ]
      })
    });
  }
}
