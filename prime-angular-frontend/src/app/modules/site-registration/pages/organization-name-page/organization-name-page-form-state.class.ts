import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Organization } from '@registration/shared/models/organization.model';

interface OrganizationNamePageDataModel extends Pick<Organization, 'id' | 'name' | 'registrationId' | 'doingBusinessAs'> { }

export class OrganizationNamePageFormState extends AbstractFormState<OrganizationNamePageDataModel> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get name(): FormControl {
    return this.formInstance.get('name') as FormControl;
  }

  public get registrationId(): FormControl {
    return this.formInstance.get('registrationId') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.formInstance.get('doingBusinessAs') as FormControl;
  }

  public get json(): OrganizationNamePageDataModel {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(organization: OrganizationNamePageDataModel): void {
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
      // TODO refactor and remove ID from the form state and add in form state service
      id: [0, []],
      name: [null, [Validators.required]],
      registrationId: [{ value: null, disabled: true }, [Validators.required]],
      doingBusinessAs: [null, []]
    });
  }
}
