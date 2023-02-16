import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteInformationForm } from './site-information-form.model';

export class SiteInformationFormState extends AbstractFormState<SiteInformationForm> {
  private siteId: number;

  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get siteName(): FormControl {
    return this.formInstance.get('siteName') as FormControl;
  }

  public get pec(): FormControl {
    return this.formInstance.get('pec') as FormControl;
  }

  public get securityGroupCode(): FormControl {
    return this.formInstance.get('securityGroupCode') as FormControl;
  }

  public get json(): SiteInformationForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public get physicalAddress(): FormGroup {
    return this.formInstance.get('physicalAddress') as FormGroup;
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
      activeBeforeRegistration: [false, []],
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
