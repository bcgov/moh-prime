import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address } from '@shared/models/address.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { Party } from '@registration/shared/models/party.model';
import { OrganizationSigningAuthorityFormState } from '@registration/pages/organization-signing-authority/organization-signing-authority-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormStateService<Organization> {
  public organizationSigningAuthorityFormState: OrganizationSigningAuthorityFormState;
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
    const signingAuthority = this.formUtilsService.toPersonJson<Party>(this.organizationSigningAuthorityFormState.json, 'mailingAddress');
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
      this.organizationSigningAuthorityFormState.form,
      this.organizationNameForm
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  protected buildForms() {
    this.organizationSigningAuthorityFormState = new OrganizationSigningAuthorityFormState(this.fb, this.formUtilsService);
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
    organization.signingAuthority.verifiedAddress = new Address();

    this.organizationNameForm.patchValue(organization);
    this.formUtilsService.toPersonFormModel<Party>([this.organizationSigningAuthorityFormState.form, organization.signingAuthority]);
  }


  /**
   * Form Builders and Helpers
   */

  private buildOrganizationNameForm(): FormGroup {
    return this.fb.group({
      // OrganizationName is the only form that contains
      // the organization ID
      id: [0, []],
      name: [null, [Validators.required]],
      registrationId: [{ value: null, disabled: true }, [Validators.required]],
      doingBusinessAs: [null, []]
    });
  }

  private buildOrganizationAgreementForm(): FormGroup {
    return this.fb.group({
      organizationAgreementGuid: ['', []]
    });
  }
}
