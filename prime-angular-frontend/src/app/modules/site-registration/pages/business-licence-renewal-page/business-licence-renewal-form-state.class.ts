import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceRenewalForm } from './business-licence-renewal-form.model';

export class BusinessLicenceRenewalPageFormState extends AbstractFormState<BusinessLicenceRenewalForm> {
  private businessLicence: BusinessLicence;

  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get businessLicenceGuid(): FormControl {
    return this.formInstance.get('businessLicenceGuid') as FormControl;
  }

  public get businessLicenceExpiry(): FormControl {
    return this.formInstance.get('expiryDate') as FormControl;
  }

  public get json(): BusinessLicenceRenewalForm {
    if (!this.formInstance) {
      return;
    }

    const { expiryDate, deferredLicenceReason } = this.formInstance.getRawValue();

    return {
      businessLicence: {
        ...this.businessLicence,
        expiryDate,
        deferredLicenceReason
      }
    };
  }

  public patchValue(model: BusinessLicenceRenewalForm): void {
    if (!this.formInstance) {
      return;
    }

    const { businessLicence } = model;
    // Preserve the business licence for use when
    // creating JSON format from the form
    this.businessLicence = businessLicence;

    // NOOP, Nothing needs to be patched
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: [
        // Will never be patched when the form is built, and is
        // only updated based on a document upload occurring.
        //
        // NOTE: Direct access only through getter
        '',
        [Validators.required]
      ],
      expiryDate: [
        '',
        [Validators.required]
      ]
    });
  }
}
