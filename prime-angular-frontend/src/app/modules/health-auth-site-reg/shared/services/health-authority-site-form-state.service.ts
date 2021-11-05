import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { RouteStateService } from '@core/services/route-state.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { VendorFormState } from '@health-auth/pages/vendor-page/vendor-form-state.class';
import { SiteInformationFormState } from '@health-auth/pages/site-information-page/site-information-form-state.class';
import { HealthAuthCareTypeFormState } from '@health-auth/pages/health-auth-care-type-page/health-auth-care-type-form-state.class';
import { SiteAddressFormState } from '@health-auth/pages/site-address-page/site-address-form-state.class';
import { HoursOperationFormState } from '@health-auth/pages/hours-operation-page/hours-operation-form-state.class';
import { RemoteUsersFormState } from '@health-auth/pages/remote-users-page/remote-users-form-state.class';
import { AdministratorFormState } from '@health-auth/pages/administrator-page/administrator-form-state.class';
import { TechnicalSupportFormState } from '@health-auth/pages/technical-support-page/technical-support-form-state.class';
import { HealthAuthorityService } from '@health-auth/shared/services/health-authority.service';

/**
 * @description
 * Manages the state of registering a site across the
 * entire workflow.
 *
 * NOTE: Must apply HealthAuthorityResolver to routable
 * views that consume the FormStateService, otherwise
 * HealthAuthorityService will be null.
 */
@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteFormStateService extends AbstractFormStateService<HealthAuthoritySite> {
  public vendorFormState: VendorFormState;
  public siteInformationFormState: SiteInformationFormState;
  public healthAuthCareTypeFormState: HealthAuthCareTypeFormState;
  public siteAddressFormState: SiteAddressFormState;
  public hoursOperationFormState: HoursOperationFormState;
  public remoteUserFormState: RemoteUsersFormState;
  public administratorFormState: AdministratorFormState;
  public technicalSupportFormState: TechnicalSupportFormState;

  /**
   * @description
   * Reference to patch health authority site properties not
   * contained within the form states.
   */
  private healthAuthoritySiteReference: Pick<HealthAuthoritySite,
    'id' |
    'healthAuthorityOrganizationId' |
    'completed' |
    'submittedDate' |
    'approvedDate' |
    'status'>;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
    // Must apply HealthAuthorityResolver to routable views that
    // consume the FormStateService, otherwise this will be null
    private healthAuthorityService: HealthAuthorityService,
    private formUtilsService: FormUtilsService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([HealthAuthSiteRegRoutes.SITE_MANAGEMENT]);
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(healthAuthoritySite: HealthAuthoritySite, forcePatch: boolean = false): void {
    if (!healthAuthoritySite) {
      return;
    }

    this.storeHealthAuthorityReference(healthAuthoritySite);

    super.setForm(healthAuthoritySite, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): HealthAuthoritySite {
    const vendorFormState = this.vendorFormState.json;
    const siteInformationFormState = this.siteInformationFormState.json;
    const healthAuthCareTypeFormState = this.healthAuthCareTypeFormState.json;
    const siteAddressFormState = this.siteAddressFormState.json;
    const hoursOperationFormState = this.hoursOperationFormState.json;
    const remoteUserFormState = this.remoteUserFormState.json;
    const administratorFormState = this.administratorFormState.json;
    const technicalSupportFormState = this.technicalSupportFormState.json;

    return HealthAuthoritySite.toHealthAuthoritySite({
      ...this.healthAuthoritySiteReference,
      ...vendorFormState,
      ...siteInformationFormState,
      ...healthAuthCareTypeFormState,
      ...siteAddressFormState,
      ...hoursOperationFormState,
      ...remoteUserFormState,
      ...administratorFormState,
      ...technicalSupportFormState
    });
  }

  /**
   * @description
   * Helper for getting a list of forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.vendorFormState.form,
      this.siteInformationFormState.form,
      this.healthAuthCareTypeFormState.form,
      this.siteAddressFormState.form,
      this.hoursOperationFormState.form,
      this.remoteUserFormState.form,
      this.administratorFormState.form,
      this.technicalSupportFormState.form
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  protected buildForms(): void {
    this.vendorFormState = new VendorFormState(this.fb, this.healthAuthorityService);
    this.siteInformationFormState = new SiteInformationFormState(this.fb);
    this.healthAuthCareTypeFormState = new HealthAuthCareTypeFormState(this.fb, this.healthAuthorityService);
    this.siteAddressFormState = new SiteAddressFormState(this.fb, this.formUtilsService);
    this.hoursOperationFormState = new HoursOperationFormState(this.fb);
    this.remoteUserFormState = new RemoteUsersFormState(this.fb);
    this.administratorFormState = new AdministratorFormState(this.fb);
    this.technicalSupportFormState = new TechnicalSupportFormState(this.fb);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(healthAuthoritySite: HealthAuthoritySite): void {
    if (!healthAuthoritySite) {
      return;
    }

    this.vendorFormState.patchValue(healthAuthoritySite);
    this.siteInformationFormState.patchValue(healthAuthoritySite);
    this.healthAuthCareTypeFormState.patchValue(healthAuthoritySite);
    this.siteAddressFormState.patchValue(healthAuthoritySite);
    this.hoursOperationFormState.patchValue(healthAuthoritySite);
    this.remoteUserFormState.patchValue(healthAuthoritySite);
    this.administratorFormState.patchValue(healthAuthoritySite);
    this.technicalSupportFormState.patchValue(healthAuthoritySite);
  }

  /**
   * @description
   * Store a reference to health authority site properties not
   * contained within the form states..
   */
  private storeHealthAuthorityReference(healthAuthoritySite: HealthAuthoritySite) {
    const {
      id,
      healthAuthorityOrganizationId,
      completed,
      submittedDate,
      approvedDate,
      status
    } = healthAuthoritySite;
    this.healthAuthoritySiteReference = {
      id,
      healthAuthorityOrganizationId,
      completed,
      submittedDate,
      approvedDate,
      status
    };
  }
}
