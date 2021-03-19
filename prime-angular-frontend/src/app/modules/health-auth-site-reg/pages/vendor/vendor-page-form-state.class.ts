import { FormBuilder, FormControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Site } from '@registration/shared/models/site.model';

interface VendorPageDataModel extends Pick<Site, 'siteVendors'> { }

export class VendorPageFormState extends AbstractFormState<VendorPageDataModel> {
  private siteId: number;

  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get vendorCode(): FormControl {
    return this.formInstance.get('vendorCode') as FormControl;
  }

  public get json(): VendorPageDataModel {
    if (!this.formInstance) {
      return;
    }

    const { vendorCode } = this.formInstance.getRawValue();
    // TODO only use a single vendor in backend view model, and drop array since you can only have one
    const siteVendors = [{
      siteId: this.siteId,
      vendorCode
    }];

    return { siteVendors };
  }

  public patchValue({ siteVendors }: VendorPageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    let vendorCode = 0;
    if (siteVendors?.length) {
      const { siteId, vendorCode: code } = siteVendors[0];
      this.siteId = siteId;
      vendorCode = code;
    }

    this.formInstance.patchValue({ vendorCode });
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
