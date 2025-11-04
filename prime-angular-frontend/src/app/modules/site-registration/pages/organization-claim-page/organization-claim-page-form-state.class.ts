import { UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { OrganizationClaimFormModel } from '@registration/shared/models/organization-claim-form.model';

export class OrganizationClaimPageFormState extends AbstractFormState<OrganizationClaimFormModel> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get pec(): UntypedFormControl {
    return this.formInstance.get('pec') as UntypedFormControl;
  }

  public get claimDetail(): UntypedFormControl {
    return this.formInstance.get('claimDetail') as UntypedFormControl;
  }

  public get json(): OrganizationClaimFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(organization: OrganizationClaimFormModel): void {
    if (!this.formInstance) {
      return;
    }
    this.formInstance.patchValue(organization);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      pec: [null, [Validators.required]],
      claimDetail: [null, [Validators.required]]
    });
  }
}
