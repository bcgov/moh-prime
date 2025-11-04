import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { AbsenceManagementForm } from './absence-management-form.model';

export class AbsenceManagementFormState extends AbstractFormState<AbsenceManagementForm> {
  public constructor(
    private fb: UntypedFormBuilder,
  ) {
    super();

    this.buildForm();
  }

  public get range(): UntypedFormGroup {
    return this.form.get('range') as UntypedFormGroup;
  }

  public get start(): UntypedFormControl {
    return this.range.get('start') as UntypedFormControl;
  }

  public get end(): UntypedFormControl {
    return this.range.get('end') as UntypedFormControl;
  }

  public get email(): UntypedFormControl {
    return this.form.get('email') as UntypedFormControl;
  }

  public get json(): AbsenceManagementForm {
    if (!this.formInstance) {
      return;
    }
    const absence = this.formInstance.getRawValue();
    return {
      start: absence.range.start,
      end: absence.range.end,
      email: absence.email
    };
  }

  public patchValue(model: AbsenceManagementForm): void {
    if (!this.formInstance) {
      return;
    }

    this.start.setValue(model.start);
    this.end.setValue(model.end);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      range: this.fb.group(
        {
          start: ['', [Validators.required]],
          end: ['', []]
        }),
      email: ['', [Validators.email]]
    });
  }
}
