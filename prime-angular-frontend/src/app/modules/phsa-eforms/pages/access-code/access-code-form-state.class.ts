import { UntypedFormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

export interface AccessCodeFormModel {
  accessCode: string;
}

export class AccessCodeFormState extends AbstractFormState<AccessCodeFormModel> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): AccessCodeFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(): void {
    throw new Error('Not Implemented');
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      accessCode: [
        '',
        [Validators.required, Validators.pattern(/^crabapples$/)]
      ]
    });
  }
}
