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

  public get siteId(): FormControl {
    return this.formInstance.get('siteId') as FormControl;
  }

  public get securityGroup(): FormControl {
    return this.formInstance.get('securityGroup') as FormControl;
  }

  public get json(): any {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(data: SiteInformationForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(data);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      siteName: ['', [Validators.required]],
      siteId: [null, [Validators.required]],
      securityGroup: [null, [Validators.required]]
    });
  }
}
