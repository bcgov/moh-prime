import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Site } from '@registration/shared/models/site.model';

interface BusinessLicencePageDataModel extends Pick<Site, 'doingBusinessAs' | 'pec'> { }

export class BusinessLicencePageFormState extends AbstractFormState<BusinessLicencePageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get businessLicenceGuid(): FormControl {
    return this.form.get('businessLicenceGuid') as FormControl;
  }

  public get deferredLicenceReason(): FormControl {
    return this.form.get('deferredLicenceReason') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
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
  public get json(): BusinessLicencePageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: BusinessLicencePageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: [
        '',
        []
      ],
      deferredLicenceReason: [
        '',
        []
      ],
      doingBusinessAs: [
        '',
        [Validators.required]
      ],
      pec: [
        null,
        [Validators.required]
      ]
    });
  }
}
