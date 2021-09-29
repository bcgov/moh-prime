import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Site } from '@registration/shared/models/site.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';

interface BusinessLicenceRenewalPageDataModel extends Pick<Site, 'doingBusinessAs' | 'pec'> { }

export class BusinessLicenceRenewalPageFormState extends AbstractFormState<BusinessLicenceRenewalPageDataModel> {
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

  public get json(): BusinessLicenceRenewalPageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: BusinessLicenceRenewalPageDataModel & { businessLicence: BusinessLicence; }): void {
    if (!this.formInstance) {
      return;
    }

    // NOOP, Nothing needs to be patched
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: [
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
