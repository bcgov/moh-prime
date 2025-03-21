import { Injectable } from '@angular/core';
import { AbstractControl, UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { RouteStateService } from '@core/services/route-state.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { SiteInformationFormState } from '@health-auth/pages/site-information-page/site-information-form-state.class';
import { HealthAuthCareTypeFormState } from '@health-auth/pages/health-auth-care-type-page/health-auth-care-type-form-state.class';
import { HoursOperationFormState } from '@health-auth/pages/hours-operation-page/hours-operation-form-state.class';
import { AdministratorFormState } from '@health-auth/pages/administrator-page/administrator-form-state.class';
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
  public siteInformationFormState: SiteInformationFormState;
  public healthAuthCareTypeFormState: HealthAuthCareTypeFormState;
  public hoursOperationFormState: HoursOperationFormState;
  public administratorFormState: AdministratorFormState;

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
    protected fb: UntypedFormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
    // Must apply HealthAuthorityResolver to routable views that
    // consume the FormStateService, otherwise this will be null
    private healthAuthorityService: HealthAuthorityService,
    private formUtilsService: FormUtilsService,
    private siteResource: SiteResource
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
    const siteInformationFormState = this.siteInformationFormState.json;
    const healthAuthCareTypeFormState = this.healthAuthCareTypeFormState.json;
    const hoursOperationFormState = this.hoursOperationFormState.json;
    const administratorFormState = this.administratorFormState.json;

    return HealthAuthoritySite.toHealthAuthoritySite({
      ...this.healthAuthoritySiteReference,
      ...siteInformationFormState,
      ...healthAuthCareTypeFormState,
      ...hoursOperationFormState,
      ...administratorFormState,
      currentSubmission: null
    });
  }

  /**
   * @description
   * Helper for getting a list of forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.siteInformationFormState.form,
      this.healthAuthCareTypeFormState.form,
      this.hoursOperationFormState.form,
      this.administratorFormState.form,
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  protected buildForms(): void {
    this.siteInformationFormState = new SiteInformationFormState(this.fb, this.formUtilsService);
    this.healthAuthCareTypeFormState = new HealthAuthCareTypeFormState(this.fb, this.healthAuthorityService);
    this.hoursOperationFormState = new HoursOperationFormState(this.fb);
    this.administratorFormState = new AdministratorFormState(this.fb);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(healthAuthoritySite: HealthAuthoritySite): void {
    if (!healthAuthoritySite) {
      return;
    }
    this.siteInformationFormState.patchValue(healthAuthoritySite, healthAuthoritySite.id);
    this.healthAuthCareTypeFormState.patchValue(healthAuthoritySite);
    this.hoursOperationFormState.patchValue(healthAuthoritySite);
    this.administratorFormState.patchValue(healthAuthoritySite);

    this.markAsPristine();
  }

  public patchSiteInformationForm(healthAuthoritySite: HealthAuthoritySite) {
    this.siteInformationFormState.patchValue(healthAuthoritySite, healthAuthoritySite.id);
  }

  public patchHoursOperationForm(healthAuthoritySite: HealthAuthoritySite) {
    this.hoursOperationFormState.businessDays.controls.forEach((businessDay: UntypedFormGroup) => {
      businessDay.get('startTime').reset();
      businessDay.get('endTime').reset();
    });
    this.hoursOperationFormState.patchValue(healthAuthoritySite);
  }

  public patchAdmintratorForm(healthAuthoritySite: HealthAuthoritySite) {
    this.administratorFormState = new AdministratorFormState(this.fb);
    this.administratorFormState.patchValue(healthAuthoritySite);
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

  /**
   * @description
   * Rather than changing the implementation at the AbstractFormStateService-level (a high-risk change),
   * override the behavior at the HealthAuthoritySiteFormStateService-level.
   */
  public get isValidSubmission(): boolean {
    return this.forms
      .reduce((valid: boolean, form: AbstractControl) =>
        // A disabled form (without errors) is also considered valid for form submission purposes
        valid && (form.valid || (form.disabled && form.errors === null)), true);
  }
}
