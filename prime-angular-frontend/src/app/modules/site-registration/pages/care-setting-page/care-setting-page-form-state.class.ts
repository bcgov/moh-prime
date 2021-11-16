import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Site } from '@registration/shared/models/site.model';

interface CareSettingPageDataModel extends Pick<Site, 'careSettingCode' | 'siteVendors'> {}

export class CareSettingPageFormState extends AbstractFormState<CareSettingPageDataModel> {
  private siteId: number;

  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get careSettingCode(): FormControl {
    return this.formInstance.get('careSettingCode') as FormControl;
  }

  public get vendorCode(): FormControl {
    return this.formInstance.get('vendorCode') as FormControl;
  }

  public get json(): CareSettingPageDataModel {
    if (!this.formInstance) {
      return;
    }

    const { careSettingCode, vendorCode } = this.formInstance.getRawValue();
    // TODO only use a single vendor, should look at dropping vendors for vendor
    const siteVendors = [{
      siteId: this.siteId,
      vendorCode
    }];

    return { careSettingCode, siteVendors };
  }

  public patchValue({ careSettingCode, siteVendors }: CareSettingPageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    let vendorCode = 0;
    if (siteVendors?.length) {
      const { siteId, vendorCode: code } = siteVendors[0];
      this.siteId = siteId;
      vendorCode = code;
    }

    this.formInstance.patchValue({ careSettingCode, vendorCode });
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
      ]
    });
  }
}
