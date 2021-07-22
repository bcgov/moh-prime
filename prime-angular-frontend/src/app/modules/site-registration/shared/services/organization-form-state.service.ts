import { Injectable } from '@angular/core';
import { FormBuilder, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { RouteStateService } from '@core/services/route-state.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationSigningAuthorityPageFormState } from '@registration/pages/organization-signing-authority-page/organization-signing-authority-page-form-state.class';
import { OrganizationNamePageFormState } from '@registration/pages/organization-name-page/organization-name-page-form-state.class';
import { OrganizationAgreementPageFormState } from '@registration/pages/organization-agreement-page/organization-agreement-page-form-state.class';
import { OrganizationClaimPageFormState } from '@registration/pages/organization-claim-page/organization-claim-page-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormStateService<Organization> {
  public organizationSigningAuthorityPageFormState: OrganizationSigningAuthorityPageFormState;
  public organizationNamePageFormState: OrganizationNamePageFormState;
  public organizationAgreementPageFormState: OrganizationAgreementPageFormState;
  public organizationClaimPageFormState: OrganizationClaimPageFormState;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
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
    // OrganizationName is the only form that contains the organization ID
    // TODO added type aliasing x 2 to stop typing errors, but needs to be fixed
    const organizationName = this.organizationNamePageFormState.json as Organization;
    const signingAuthority = this.organizationSigningAuthorityPageFormState.json;
    const { organizationAgreementGuid } = this.organizationAgreementPageFormState.json;

    return {
      ...organizationName,
      signingAuthorityId: signingAuthority?.id, // TODO can this be dropped?
      signingAuthority,
      organizationAgreementGuid
    } as Organization;
  }

  /**
   * @description
   * List of constituent model forms, which is used at minimum to
   * drive internal form helper methods.
   */
  public get forms(): AbstractControl[] {
    return [
      this.organizationSigningAuthorityPageFormState.form,
      this.organizationNamePageFormState.form
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  protected buildForms() {
    this.organizationSigningAuthorityPageFormState = new OrganizationSigningAuthorityPageFormState(this.fb, this.formUtilsService);
    this.organizationNamePageFormState = new OrganizationNamePageFormState(this.fb);
    this.organizationAgreementPageFormState = new OrganizationAgreementPageFormState(this.fb);
    this.organizationClaimPageFormState = new OrganizationClaimPageFormState(this.fb);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   *
   * NOTE: OrganizationAgreementPageFormState does not need to be patched.
   */
  protected patchForm(organization: Organization): void {
    if (!organization) {
      return;
    }

    const { id, name, registrationId, doingBusinessAs, signingAuthority } = organization;

    this.organizationSigningAuthorityPageFormState.patchValue(signingAuthority);
    this.organizationNamePageFormState.patchValue({ id, name, registrationId, doingBusinessAs });
  }
}
