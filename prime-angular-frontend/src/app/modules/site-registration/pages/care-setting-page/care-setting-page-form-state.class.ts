import { UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Site } from '@registration/shared/models/site.model';

interface CareSettingPageDataModel extends Pick<Site, 'careSettingCode' | 'siteVendors'> { }

export class CareSettingPageFormState extends AbstractFormState<CareSettingPageDataModel> {
  private siteId: number;

  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get careSettingCode(): UntypedFormControl {
    return this.formInstance.get('careSettingCode') as UntypedFormControl;
  }

  public get vendorCode(): UntypedFormControl {
    return this.formInstance.get('vendorCode') as UntypedFormControl;
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
      ],
      isNew: [
        false,
        []
      ]
    });
  }
}
