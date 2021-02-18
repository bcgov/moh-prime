import { FormBuilder } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

export interface EnrolleeInformationPageFormModel { }

export class EnrolleeInformationPageFormState extends AbstractFormState<EnrolleeInformationPageFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): EnrolleeInformationPageFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: EnrolleeInformationPageFormModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      organization: [null, []],
      role: [null, []]
    });
  }
}
