import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { FormControlValidators } from '@lib/validators/form-control.validators';

import { Organization } from '@registration/shared/models/organization.model';
import { AbstractFormState } from '@registration/shared/classes/abstract-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormState<Organization> {
  public signingAuthorityForm: FormGroup;
  public organizationNameForm: FormGroup;

  constructor(
    protected fb: FormBuilder
  ) {
    super(fb);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Organization {
    const organizationName = this.organizationNameForm.getRawValue();
    const signingAuthority = this.toPartyJson(this.signingAuthorityForm.getRawValue(), 'mailingAddress');

    return {
      // OrganizationName is the only form that contains the organization ID
      ...organizationName,
      signingAuthorityId: signingAuthority?.id,
      signingAuthority
    };
  }

  /**
   * @description
   * List of constituent model forms, which is used at minimum to
   * drive internal form helper methods.
   */
  public get forms(): AbstractControl[] {
    return [
      this.signingAuthorityForm,
      this.organizationNameForm
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  public init() {
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.organizationNameForm = this.buildOrganizationNameForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(organization: Organization): Organization {
    if (!organization) {
      return null;
    }

    this.organizationNameForm.patchValue(organization);
    this.toPartyFormModel([this.signingAuthorityForm, organization.signingAuthority]);
  }

  // TODO BCSC information is also in enrolments and can have shared form helpers
  private buildSigningAuthorityForm(): FormGroup {
    // Prevent BCSC information from being changed
    return this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        { value: null, disabled: true },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled: true },
        [Validators.required]
      ],
      preferredFirstName: [
        null, []
      ],
      preferredMiddleName: [
        null, []
      ],
      preferredLastName: [
        null, []
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      fax: [
        null,
        [FormControlValidators.phone]
      ],
      smsPhone: [
        null,
        [FormControlValidators.phone]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      physicalAddress: this.buildAddressForm({
        areDisabled: ['street', 'street2', 'city', 'provinceCode', 'countryCode', 'postal'],
      }),
      mailingAddress: this.buildAddressForm()
    });
  }

  private buildOrganizationNameForm(): FormGroup {
    return this.fb.group({
      // OrganizationName is the only form that contains
      // the organization ID
      id: [
        0,
        []
      ],
      name: [
        null,
        [Validators.required]
      ],
      registrationId: [
        { value: null, disabled: true },
        [Validators.required]
      ],
      doingBusinessAs: [
        null,
        []
      ]
    });
  }
}
