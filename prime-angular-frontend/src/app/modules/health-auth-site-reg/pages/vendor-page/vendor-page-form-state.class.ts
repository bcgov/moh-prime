import { FormBuilder, FormControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export interface VendorForm {
  vendorCode: number;
}

export class VendorPageFormState extends AbstractFormState<VendorForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get vendorCode(): FormControl {
    return this.formInstance.get('vendorCode') as FormControl;
  }

  public get json(): VendorForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(vendorForm: VendorForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(vendorForm);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      vendorCode: [
        0,
        [FormControlValidators.requiredIndex]
      ]
    });
  }
}
