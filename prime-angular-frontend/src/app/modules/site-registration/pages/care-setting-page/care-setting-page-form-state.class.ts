import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Vendor } from '@registration/shared/models/vendor.model';

interface CareSettingModel {
  careSettingCode: number;
  siteVendors: Vendor[];
  pec: string;
}

interface CareSettingPageFormModel {
  careSettingCode: number;
  vendorCode: number;
  pec: string;
}

export class CareSettingPageFormState extends AbstractFormState<CareSettingModel> {
  private siteId: number;

  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): CareSettingModel {
    if (!this.formInstance) {
      return;
    }

    const { careSettingCode, vendorCode, pec } = this.formInstance.getRawValue();
    // TODO only use a single vendor, should look at dropping vendors for vendor
    const siteVendors = [{
      siteId: this.siteId,
      vendorCode
    }];

    return { careSettingCode, siteVendors, pec };
  }

  public patchValue({ careSettingCode, siteVendors, pec }: CareSettingModel): void {
    if (!this.formInstance) {
      return;
    }

    let vendorCode = 0;
    if (siteVendors?.length) {
      const { siteId, vendorCode: code } = siteVendors[0];
      this.siteId = siteId;
      vendorCode = code;
    }

    this.formInstance.patchValue({ careSettingCode, vendorCode, pec });
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      careSettingCode: [
        null,
        [Validators.required]
      ],
      vendorCode: [
        0,
        [FormControlValidators.requiredIndex]
      ],
      pec: [
        null,
        [Validators.required]
      ]
    });
  }
}
