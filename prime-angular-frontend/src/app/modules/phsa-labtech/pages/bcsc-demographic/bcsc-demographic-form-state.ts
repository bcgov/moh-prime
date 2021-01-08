import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export interface PhsaBcscDemographicFormModel {
  phone: string;
  phoneExtension: string;
  email: string;
};

export class BcscDemographicFormState extends AbstractFormState<PhsaBcscDemographicFormModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): PhsaBcscDemographicFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(): void {
    throw new Error('Not Implemented');
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      phoneExtension: [
        null,
        [FormControlValidators.numeric]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ]
    });
  }
}
