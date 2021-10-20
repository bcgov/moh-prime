import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { HealthAuthCareTypeForm } from './health-auth-care-type-form.model';

export class HealthAuthCareTypeFormState extends AbstractFormState<HealthAuthCareTypeForm> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get careType(): FormControl {
    return this.formInstance.get('careType') as FormControl;
  }

  public get json(): HealthAuthCareTypeForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: HealthAuthCareTypeForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      careType: [
        null,
        [Validators.required]
      ]
    });
  }
}
