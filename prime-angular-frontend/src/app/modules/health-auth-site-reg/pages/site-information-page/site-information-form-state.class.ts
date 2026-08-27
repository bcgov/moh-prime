import { UntypedFormBuilder, UntypedFormControl, Validators, UntypedFormGroup } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteInformationForm } from './site-information-form.model';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export class SiteInformationFormState extends AbstractFormState<SiteInformationForm> {
  private siteId: number;

  public constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get siteName(): UntypedFormControl {
    return this.formInstance.get('siteName') as UntypedFormControl;
  }

  public get mnemonic(): UntypedFormControl {
    return this.formInstance.get('mnemonic') as UntypedFormControl;
  }

  public get pec(): UntypedFormControl {
    return this.formInstance.get('pec') as UntypedFormControl;
  }

  public get pharmacyPhone(): UntypedFormControl {
    return this.formInstance.get('pharmacyPhone') as UntypedFormControl;
  }

  public get securityGroupCode(): UntypedFormControl {
    return this.formInstance.get('securityGroupCode') as UntypedFormControl;
  }

  public get json(): SiteInformationForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public get physicalAddress(): UntypedFormGroup {
    return this.formInstance.get('physicalAddress') as UntypedFormGroup;
  }

  public patchValue(model: SiteInformationForm, siteId: number): void {
    if (!this.formInstance) {
      return;
    }

    this.siteId = siteId;
    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      siteName: ['', [Validators.required]],
      pec: [null, []],
      mnemonic: [null, []],
      pharmacyPhone: [null, [FormControlValidators.phone]],
      securityGroupCode: [null, [Validators.required]],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        areDisabled: ['provinceCode', 'countryCode'],
        useDefaults: ['provinceCode', 'countryCode'],
        exclude: ['street2']
      })
    });
  }
}
