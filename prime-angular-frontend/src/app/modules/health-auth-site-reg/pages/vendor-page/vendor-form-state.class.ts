import { FormBuilder, FormControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { VendorForm } from './vendor-form.model';

export class VendorFormState extends AbstractFormState<VendorForm> {
  public constructor(
    private fb: FormBuilder,
    private healthAuthorityService
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
    const { healthAuthorityVendorId } = this.formInstance.getRawValue();
    const healthAuthorityVendor = this.healthAuthorityService.healthAuthority.vendors
      .find(hav => hav.id === healthAuthorityVendorId);

    return { healthAuthorityVendor };
  }

  public patchValue(model: VendorForm): void {
    const healthAuthorityVendorId = model.healthAuthorityVendor?.id;
    if (!this.formInstance || !healthAuthorityVendorId) {
      return;
    }

    this.formInstance.patchValue({ healthAuthorityVendorId });
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
