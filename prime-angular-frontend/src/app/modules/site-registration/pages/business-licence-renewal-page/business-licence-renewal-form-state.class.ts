import { UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceRenewalForm } from './business-licence-renewal-form.model';

export class BusinessLicenceRenewalPageFormState extends AbstractFormState<BusinessLicenceRenewalForm> {
  private businessLicence: BusinessLicence;

  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get filename(): UntypedFormControl {
    return this.formInstance.get('filename') as UntypedFormControl;
  }

  public get businessLicenceGuid(): UntypedFormControl {
    return this.formInstance.get('businessLicenceGuid') as UntypedFormControl;
  }

  public get businessLicenceExpiry(): UntypedFormControl {
    return this.formInstance.get('expiryDate') as UntypedFormControl;
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
      filename: [null, []],
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
