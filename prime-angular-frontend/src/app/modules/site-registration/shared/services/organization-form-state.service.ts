import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationSigningAuthorityPageFormState } from '@registration/pages/organization-signing-authority-page/organization-signing-authority-page-form-state.class';
import { OrganizationNameFormState } from '@registration/pages/organization-name/organization-name-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormStateService<Organization> {
  public organizationSigningAuthorityFormState: OrganizationSigningAuthorityPageFormState;
  public organizationNameFormState: OrganizationNameFormState;
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
  // TODO added type aliasing x 2 to stop typing errors, but needs to be fixed
  // TODO possibly add an Organization DTO, form, or update model
  // TODO refactor organizationAgreementGuid use or change return type
  public get json(): Organization {
    // OrganizationName is the only form that contains the organization ID
    const organizationName = this.organizationNameFormState.json as Organization;
    const signingAuthority = this.organizationSigningAuthorityFormState.json;
    const { organizationAgreementGuid } = this.organizationAgreementForm.getRawValue();

    return {
      ...organizationName,
      signingAuthorityId: signingAuthority?.id,
      signingAuthority,
      organizationAgreementGuid // TODO feels like this shouldn't be stored in the form
    } as Organization;
  }

  /**
   * @description
   * List of constituent model forms, which is used at minimum to
   * drive internal form helper methods.
   */
  public get forms(): AbstractControl[] {
    return [
      this.organizationSigningAuthorityFormState.form,
      this.organizationNameFormState.form
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  protected buildForms() {
    this.organizationSigningAuthorityFormState = new OrganizationSigningAuthorityPageFormState(this.fb, this.formUtilsService);
    this.organizationNameFormState = new OrganizationNameFormState(this.fb);
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

    const { id, name, registrationId, doingBusinessAs, signingAuthority } = organization;

    this.organizationSigningAuthorityFormState.patchValue(signingAuthority);
    this.organizationNameFormState.patchValue({ id, name, registrationId, doingBusinessAs });
  }

  /**
   * Form Builders and Helpers
   */

  private buildOrganizationAgreementForm(): FormGroup {
    return this.fb.group({
      organizationAgreementGuid: ['', []]
    });
  }
}
