import { FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

export interface SiteAddressPageFormModel {
  physicalAddress: Address;
}

export class SiteAddressPageFormState extends AbstractFormState<SiteAddressPageFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): SiteAddressPageFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: SiteAddressPageFormModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        areDisabled: ['provinceCode', 'countryCode'],
        useDefaults: ['provinceCode', 'countryCode'],
        exclude: ['street2']
      })
    });
  }
}
