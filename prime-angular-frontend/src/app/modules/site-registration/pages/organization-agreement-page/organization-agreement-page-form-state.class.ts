import { FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

interface OrganizationAgreementPageDataModel {
  organizationAgreementGuid: string;
}

export class OrganizationAgreementPageFormState extends AbstractFormState<OrganizationAgreementPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): OrganizationAgreementPageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(_: OrganizationAgreementPageDataModel): void {
    throw new Error('Organization agreement should never have the form patched');
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      organizationAgreementGuid: ['', []]
    });
  }
}
