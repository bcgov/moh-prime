import { FormBuilder, FormGroup } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

export class SiteAddressPageFormState extends AbstractFormState<Address> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get name(): FormGroup {
    return this.formInstance.get('name') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.formInstance.get('physicalAddress') as FormGroup;
  }

  public get json(): Address {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue().physicalAddress;
  }

  public patchValue(physicalAddress: Address): void {
    if (!this.formInstance || !physicalAddress) {
      return;
    }

    this.formInstance.patchValue({ physicalAddress });
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
