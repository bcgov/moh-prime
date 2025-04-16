import { UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Organization } from '@registration/shared/models/organization.model';

interface OrganizationNamePageDataModel extends Pick<Organization, 'id' | 'name' | 'registrationId' | 'doingBusinessAs'> { }

export class OrganizationNamePageFormState extends AbstractFormState<OrganizationNamePageDataModel> {
  public constructor(
    private fb: UntypedFormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get name(): UntypedFormControl {
    return this.formInstance.get('name') as UntypedFormControl;
  }

  public get registrationId(): UntypedFormControl {
    return this.formInstance.get('registrationId') as UntypedFormControl;
  }

  public get doingBusinessAs(): UntypedFormControl {
    return this.formInstance.get('doingBusinessAs') as UntypedFormControl;
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
