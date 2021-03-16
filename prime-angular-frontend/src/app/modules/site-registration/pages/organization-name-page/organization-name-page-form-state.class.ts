import { FormBuilder, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Organization } from '@registration/shared/models/organization.model';

export interface OrganizationNamePageFormModel extends Pick<Organization, 'id' | 'name' | 'registrationId' | 'doingBusinessAs'> { }

export class OrganizationNamePageFormState extends AbstractFormState<OrganizationNamePageFormModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get json(): OrganizationNamePageFormModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(organization: OrganizationNamePageFormModel): void {
    if (!this.formInstance) {
      return;
    }
    this.formInstance.patchValue(organization);
  }

  // TODO BCSC information form reuse for sharing between enrolment and PHSA
  public buildForm(): void {
    // Prevent BCSC information from being changed
    this.formInstance = this.fb.group({
      // OrganizationName is the only form that contains
      // the organization ID
      id: [0, []],
      name: [null, [Validators.required]],
      registrationId: [{ value: null, disabled: true }, [Validators.required]],
      doingBusinessAs: [null, []]
    });
  }
}
