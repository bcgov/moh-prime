import { UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { NextStepsForm } from './next-steps-form.model';

export class NextStepsFormState extends AbstractFormState<NextStepsForm> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get emails(): UntypedFormControl {
    return this.form.get('emails') as UntypedFormControl;
  }

  public get json(): NextStepsForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: NextStepsForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      emails: [null, [
        Validators.required,
        FormControlValidators.multipleEmails
      ]]
    });
  }
}
