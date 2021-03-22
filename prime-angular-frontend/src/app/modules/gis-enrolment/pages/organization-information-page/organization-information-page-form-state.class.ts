import { FormBuilder, Validators } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

export interface OrganizationInformationPageFormModel {
  organization: string;
  role: string;
}

export class OrganizationInformationPageFormState extends AbstractFormState<OrganizationInformationPageFormModel> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): OrganizationInformationPageFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: OrganizationInformationPageFormModel): void {
    if (!this.formInstance) {
      return;
    }

    this.formInstance.patchValue(model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      organization: [null, [Validators.required]],
      role: [null, [Validators.required]]
    });
  }
}
