import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

interface OrganizationInformationPageDataModel {
  organization: string;
  role: string;
}

export class OrganizationInformationPageFormState extends AbstractFormState<OrganizationInformationPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get organization(): FormControl {
    return this.formInstance.get('organization') as FormControl;
  }

  public get role(): FormControl {
    return this.formInstance.get('role') as FormControl;
  }

  public get json(): OrganizationInformationPageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: OrganizationInformationPageDataModel): void {
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
