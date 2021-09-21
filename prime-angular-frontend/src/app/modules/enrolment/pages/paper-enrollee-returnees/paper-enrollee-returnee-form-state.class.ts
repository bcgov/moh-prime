import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Enrollee } from '@shared/models/enrollee.model';

export class PaperEnrolleeReturneeFormState extends AbstractFormState<Enrollee> {
  public constructor(
    private fb: FormBuilder,
  ) {
    super();

    this.buildForm();
  }

  public get json(): Enrollee {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(enrollee: Enrollee): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(enrollee);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      gpid: [null, Validators.required],
    });
  }

}
