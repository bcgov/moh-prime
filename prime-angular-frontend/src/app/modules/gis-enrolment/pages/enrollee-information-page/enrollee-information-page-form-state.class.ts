import { UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

export interface EnrolleeInformationPageDataModel {
  email: string;
  phone: string;
}

export class EnrolleeInformationPageFormState extends AbstractFormState<EnrolleeInformationPageDataModel> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get phone(): UntypedFormControl {
    return this.formInstance.get('phone') as UntypedFormControl;
  }

  public get email(): UntypedFormControl {
    return this.formInstance.get('email') as UntypedFormControl;
  }

  public get json(): EnrolleeInformationPageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: EnrolleeInformationPageDataModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]]
    });
  }
}
