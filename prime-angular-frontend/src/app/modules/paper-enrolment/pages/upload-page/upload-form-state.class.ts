import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { UploadForm } from './upload-form.model';

export class UploadFormState extends AbstractFormState<UploadForm> {
  public constructor(
    private fb: FormBuilder,
  ) {
    super();

    this.buildForm();
  }

  public get assignedTOAType(): FormControl {
    return this.form.get('assignedTOAType') as FormControl;
  }

  public get json(): UploadForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: UploadForm): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      assignedTOAType: [null, [Validators.required]],
    });
  }
}
