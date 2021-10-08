import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

export class PaperEnrolleeReturneeFormState extends AbstractFormState<String> {
  public constructor(
    private fb: FormBuilder,
  ) {
    super();

    this.buildForm();
  }

  public get json(): String {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(userProvidedGpid: String): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(userProvidedGpid);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      gpid: [null],
    });
  }
}
