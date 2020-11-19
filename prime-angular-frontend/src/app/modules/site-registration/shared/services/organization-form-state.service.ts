import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormState<Organization> {
  public signingAuthorityForm: FormGroup;
  public organizationNameForm: FormGroup;
  public organizationAgreementForm: FormGroup;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService
  ) {
    super(fb, routeStateService, logger, [SiteRoutes.SITE_MANAGEMENT]);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Organization {
    const organizationName = this.organizationNameForm.getRawValue();
    const signingAuthority = this.toPersonJson<Party>(this.signingAuthorityForm.getRawValue(), 'mailingAddress');
    const { organizationAgreementGuid } = this.organizationAgreementForm.getRawValue();

    return {
      // OrganizationName is the only form that contains the organization ID
      ...organizationName,
      signingAuthorityId: signingAuthority?.id,
      signingAuthority,
      organizationAgreementGuid
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
  protected buildForms() {
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.organizationNameForm = this.buildOrganizationNameForm();
    this.organizationAgreementForm = this.buildOrganizationAgreementForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(organization: Organization): void {
    if (!organization) {
      return;
    }

    this.organizationNameForm.patchValue(organization);
    this.toPersonFormModel<Party>([this.signingAuthorityForm, organization.signingAuthority]);
  }


  /**
   * Form Builders and Helpers
   */

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
      mailingAddress: this.buildAddressForm(),
      dateOfBirth: [
        null,
        [Validators.required]
      ]
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
        []
      ],
      doingBusinessAs: [
        null,
        []
      ]
    });
  }

  private buildOrganizationAgreementForm(): FormGroup {
    return this.fb.group({
      organizationAgreementGuid: [
        '',
        []
      ]
    });
  }
}
