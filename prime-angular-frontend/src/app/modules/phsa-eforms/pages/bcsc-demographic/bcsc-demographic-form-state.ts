import { FormBuilder, Validators } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export interface PhsaBcscDemographicFormModel {
  phone: string;
  phoneExtension: string;
  email: string;
}

export class BcscDemographicFormState extends AbstractFormState<PhsaBcscDemographicFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
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
      validatedAddress: this.formUtilsService.buildAddressForm(),
      mailingAddress: this.formUtilsService.buildAddressForm(),
      physicalAddress: this.formUtilsService.buildAddressForm(),
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
