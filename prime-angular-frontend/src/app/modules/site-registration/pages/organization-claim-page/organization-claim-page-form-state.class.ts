import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Organization } from '@registration/shared/models/organization.model';

interface OrganizationClaimPageDataModel {
  pec: string;
  claimDetail: string;
}

export class OrganizationClaimPageFormState extends AbstractFormState<OrganizationClaimPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get pec(): FormControl {
    return this.formInstance.get('pec') as FormControl;
  }

  public get claimDetail(): FormControl {
    return this.formInstance.get('claimDetail') as FormControl;
  }

  public get json(): OrganizationClaimPageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(organization: OrganizationClaimPageDataModel): void {
    if (!this.formInstance) {
      return;
    }
    this.formInstance.patchValue(organization);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      //id: [0, []],
      pec: [null, [Validators.required]],
      claimDetail: [null, [Validators.required]]
    });
  }
}
