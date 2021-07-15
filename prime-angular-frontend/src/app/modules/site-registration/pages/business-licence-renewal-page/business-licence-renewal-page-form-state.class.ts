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

  /**
   * @description
   * Access to doingBusinessAs and pec, but prevents transmission
   * of the deferredLicenceReason and businessLicenceGuid.
   *
   * NOTE: deferredLicenceReason and businessLicenceGuid are not
   * updated using the site update endpoint, and are only used
   * within the business licence page.
   */
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

    const { businessLicence } = model;

    this.formInstance.patchValue({ deferredLicenceReason: businessLicence?.deferredLicenceReason });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: ['', [Validators.required]],
    });
  }
}
