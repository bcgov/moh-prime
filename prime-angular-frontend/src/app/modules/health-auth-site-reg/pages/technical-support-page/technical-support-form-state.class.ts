import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

import { TechnicalSupportForm } from './technical-support-form.model';

export class TechnicalSupportFormState extends AbstractFormState<TechnicalSupportForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): TechnicalSupportForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: TechnicalSupportForm): void {
    if (!model) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      healthAuthorityTechnicalSupportId: [null, [Validators.required]]
    });
  }
}
