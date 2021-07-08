import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

import { AdministratorForm } from './administrator-form.model';

export class AdministratorFormState extends AbstractFormState<AdministratorForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get pharmanetAdministratorId(): FormControl {
    return this.formInstance.get('pharmanetAdministratorId') as FormControl;
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

  public buildForm(disabled: boolean = false): void {
    this.formInstance = this.fb.group({
      pharmanetAdministratorId: [0, [Validators.required]]
    });
  }
}
