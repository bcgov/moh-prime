import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { SiteInformationForm } from './site-information-form.model';

export class SiteInformationFormState extends AbstractFormState<SiteInformationForm> {
  public constructor(
    private fb: FormBuilder
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

  public patchValue(model: SiteInformationForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      siteName: ['', [Validators.required]],
      pec: [null, [Validators.required]],
      securityGroupCode: [null, [Validators.required]]
    });
  }
}
