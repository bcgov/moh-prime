import { FormBuilder, FormControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { VendorForm } from './vendor-form.model';

export class VendorFormState extends AbstractFormState<VendorForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get healthAuthorityVendorId(): FormControl {
    return this.formInstance.get('healthAuthorityVendorId') as FormControl;
  }

  public get json(): VendorForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: VendorForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      healthAuthorityVendorId: [
        0,
        [FormControlValidators.requiredIndex]
      ]
    });
  }
}
