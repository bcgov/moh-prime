import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Site } from '@registration/shared/models/site.model';

interface OrganizationAgreementPageDataModel {
  organizationAgreementGuid: string;
}

// TODO don't know what we're doing here yet
export class OrganizationAgreementPageFormState extends AbstractFormState<OrganizationAgreementPageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get organizationAgreementGuid(): FormControl {
    return this.formInstance.get('organizationAgreementGuid') as FormControl;
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
      organizationAgreementGuid: [null, [Validators.required]]
    });
  }
}
