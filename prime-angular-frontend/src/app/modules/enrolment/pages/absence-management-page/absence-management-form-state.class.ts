import { FormBuilder, FormControl, FormGroup, FormGroupDirective, ValidationErrors, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import moment from 'moment';
import { AbsenceManagementForm } from './absence-management-form.model';

export class AbsenceManagementFormState extends AbstractFormState<AbsenceManagementForm> {
  public constructor(
    private fb: FormBuilder,
  ) {
    super();

    this.buildForm();
  }

  public get range(): FormGroup {
    return this.form.get('range') as FormGroup;
  }

  public get start(): FormControl {
    return this.range.get('start') as FormControl;
  }

  public get end(): FormControl {
    return this.range.get('end') as FormControl;
  }

  public get json(): AbsenceManagementForm {
    if (!this.formInstance) {
      return;
    }
    const absence = this.formInstance.getRawValue();
    return {
      start: absence.range.start,
      end: absence.range.end,
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
          end: ['', [Validators.required]],
        })
    });
  }
}
