import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

import { AdministratorForm } from './administrator-form.model';

export class AdministratorFormState extends AbstractFormState<AdministratorForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): AdministratorForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: AdministratorForm): void {
    if (!model) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      healthAuthorityPharmanetAdministratorId: [null, [Validators.required]]
    });
  }
}
