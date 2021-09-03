import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { Observable } from 'rxjs';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { SiteResource } from '@core/resources/site-resource.service';

import { Site } from '@registration/shared/models/site.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';

type BusinessLicencePageDataModel = Pick<Site, 'doingBusinessAs' | 'pec'>

export class BusinessLicencePageFormState extends AbstractFormState<BusinessLicencePageDataModel> {
  public constructor(
    private fb: FormBuilder,
    private siteResource: SiteResource
  ) {
    super();

    this.buildForm();
  }

  public get businessLicenceGuid(): FormControl {
    return this.formInstance.get('businessLicenceGuid') as FormControl;
  }

  public get businessLicenceExpiry(): FormControl {
    return this.formInstance.get('businessLicenceExpiry') as FormControl;
  }

  public get deferredLicenceReason(): FormControl {
    return this.formInstance.get('deferredLicenceReason') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.formInstance.get('doingBusinessAs') as FormControl;
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

  public patchValue(model: BusinessLicencePageDataModel & { businessLicence: BusinessLicence; }): void {
    if (!this.formInstance) {
      return;
    }

    const { doingBusinessAs, pec, businessLicence } = model;

    this.formInstance.patchValue({ doingBusinessAs, pec, deferredLicenceReason: businessLicence?.deferredLicenceReason });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      businessLicenceGuid: [
        '',
        []
      ],
      businessLicenceExpiry: [
        '',
        [Validators.required]
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
        [Validators.required],
        FormControlValidators.uniqueAsync(this.checkPecIsUnique())
      ]
    });
  }

  private checkPecIsUnique(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecExists(value);
  }
}
