import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { PaperEnrolleeForm } from './paper-enrollee-returnee-form.model';

export class PaperEnrolleeReturneeFormState extends AbstractFormState<PaperEnrolleeForm> {
  public constructor(
    private fb: FormBuilder,
  ) {
    super();

    this.buildForm();
  }

  public get paperEnrolleeGpid(): FormControl {
    return this.formInstance.get('paperEnrolleeGpid') as FormControl;
  }

  public get json(): PaperEnrolleeForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: PaperEnrolleeForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      paperEnrolleeGpid: [
        null,
        []
      ],
    });
  }
}
