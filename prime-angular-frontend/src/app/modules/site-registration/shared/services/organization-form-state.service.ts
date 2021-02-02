import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormStateService<Organization> {
  public signingAuthorityForm: FormGroup;
  public organizationNameForm: FormGroup;
  public organizationAgreementForm: FormGroup;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    private formUtilsService: FormUtilsService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([SiteRoutes.SITE_MANAGEMENT]);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Organization {
    const organizationName = this.organizationNameForm.getRawValue();
    const signingAuthority = this.formUtilsService.toPersonJson<Party>(this.signingAuthorityForm.getRawValue(), 'mailingAddress');
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

    console.log('TEMPORARY TO ALLOW WORK!!!');
    // TODO add to adapters so backend can send null
    organization.signingAuthority.validatedAddress = new Address();

    this.organizationNameForm.patchValue(organization);
    this.formUtilsService.toPersonFormModel<Party>([this.signingAuthorityForm, organization.signingAuthority]);
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
      validatedAddress: this.formUtilsService.buildAddressForm(),
      mailingAddress: this.formUtilsService.buildAddressForm(),
      physicalAddress: this.formUtilsService.buildAddressForm(),
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
        [Validators.required]
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
