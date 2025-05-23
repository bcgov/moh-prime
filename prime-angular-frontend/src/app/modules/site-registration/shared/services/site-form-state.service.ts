import { Injectable } from '@angular/core';
import { UntypedFormBuilder, AbstractControl, Validators } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { RouteStateService } from '@core/services/route-state.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { HoursOperationPageFormState } from '@registration/pages/hours-operation-page/hours-operation-page-form-state.class';
import { AdministratorPageFormState } from '@registration/pages/administrator-page/administrator-page-form-state.class';
import { PrivacyOfficerPageFormState } from '@registration/pages/privacy-officer-page/privacy-officer-page-form-state.class';
import { TechnicalSupportPageFormState } from '@registration/pages/technical-support-page/technical-support-page-form-state.class';
import { RemoteUsersPageFormState } from '@registration/pages/remote-users-page/remote-users-page-form-state.class';
import { CareSettingPageFormState } from '@registration/pages/care-setting-page/care-setting-page-form-state.class';
import { BusinessLicenceFormState } from '@registration/pages/business-licence-page/business-licence-form-state.class';
import { BusinessLicenceRenewalPageFormState } from '@registration/pages/business-licence-renewal-page/business-licence-renewal-form-state.class';
import { DeviceProviderPageFormState } from '@registration/pages/device-provider-page/device-provider-page-form-state.class';
import { SiteService } from './site.service';

@Injectable({
  providedIn: 'root'
})
export class SiteFormStateService extends AbstractFormStateService<Site> {
  public careSettingPageFormState: CareSettingPageFormState;
  public businessLicenceFormState: BusinessLicenceFormState;
  public hoursOperationPageFormState: HoursOperationPageFormState;
  public remoteUsersPageFormState: RemoteUsersPageFormState;
  public administratorPharmaNetFormState: AdministratorPageFormState;
  public privacyOfficerFormState: PrivacyOfficerPageFormState;
  public technicalSupportFormState: TechnicalSupportPageFormState;
  public deviceProviderFormState: DeviceProviderPageFormState;
  public businessLicenceRenewalPageFormState: BusinessLicenceRenewalPageFormState;

  private siteId: number;
  private organizationId: number;
  private provisionerId: number;

  /**
   * @description
   * Shallowly immutable reference to a selection of site properties
   * for use determining state of the site.
   */
  private site: Pick<Site, 'status' | 'completed' | 'submittedDate' | 'approvedDate' | 'careSettingCode'>;

  constructor(
    protected fb: UntypedFormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
    private formUtilsService: FormUtilsService,
    private siteResource: SiteResource,
    private siteService: SiteService,
  ) {
    super(fb, routeStateService, logger);

    this.initialize([SiteRoutes.ORGANIZATIONS]);
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(site: Site, forcePatch: boolean = false): void {
    if (!site) {
      return;
    }

    // Store required site identifiers not captured in forms
    this.siteId = site.id;
    this.organizationId = site.organizationId;
    this.provisionerId = site.provisionerId;

    const { status, completed, submittedDate, approvedDate, careSettingCode } = site;
    this.site = Object.freeze({ status, completed, submittedDate, approvedDate, careSettingCode });

    super.setForm(site, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Site {
    const { careSettingCode, siteVendors } = this.careSettingPageFormState.json;
    const { businessLicence, doingBusinessAs, pec, activeBeforeRegistration, physicalAddress, isNew, deviceProviderId } = this.businessLicenceFormState.json;
    const businessHours = this.hoursOperationPageFormState.json;
    const remoteUsers = this.remoteUsersPageFormState.json;
    const administratorPharmaNet = this.administratorPharmaNetFormState.json;
    const privacyOfficer = this.privacyOfficerFormState.json;
    const technicalSupport = this.technicalSupportFormState.json;
    const individualDeviceProviders = this.deviceProviderFormState.json;

    // Includes site related keys to uphold relationships, and allow for updates
    // to a site. Keys not for update have been omitted and the type enforced
    return {
      id: this.siteId,
      organizationId: this.organizationId,
      // organization (N/A)
      provisionerId: this.provisionerId,
      // provisioner (N/A)
      careSettingCode,
      siteVendors,
      doingBusinessAs,
      businessLicence,
      physicalAddressId: physicalAddress?.id, // TODO can this be dropped?
      physicalAddress,
      businessHours,
      remoteUsers,
      administratorPharmaNetId: administratorPharmaNet?.id, // TODO can this be dropped?
      administratorPharmaNet,
      privacyOfficerId: privacyOfficer?.id, // TODO can this be dropped?
      privacyOfficer,
      technicalSupportId: technicalSupport?.id, // TODO can this be dropped?
      technicalSupport,
      individualDeviceProviders,
      // completed (N/A)
      // approvedDate (N/A)
      // submittedDate (N/A)
      pec,
      activeBeforeRegistration,
      isNew,
      deviceProviderId
    } as Site; // Enforced type due to N/A properties
  }

  /**
   * @description
   * Helper for getting a list of organization forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.careSettingPageFormState.form,
      this.businessLicenceFormState.form,
      this.hoursOperationPageFormState.form,
      this.remoteUsersPageFormState.form,
      this.administratorPharmaNetFormState.form,
      this.privacyOfficerFormState.form,
      this.technicalSupportFormState.form,
      this.deviceProviderFormState.form
    ];
  }

  /**
   * @description
   * Check that all constituent forms are valid for submission.
   *
   * NOTE: PEC can be deferred, but the toggle is not persisted. This patch
   * will allow submission for an unapproved site that does not have a PEC.
   */
  public get isValidSubmission(): boolean {
    const isCPorPCHP = this.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST ||
      this.site.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE;

    const pecControl = this.businessLicenceFormState.pec;
    // Managed to make it through the registration without a PEC and is
    // Community Pharmacy or PCHP then assumed to indicate deferment, which is
    // not possible after the site has been approved
    const pecDeferred = this.site.completed &&
      !this.site.approvedDate &&
      !pecControl.value &&
      isCPorPCHP;

    // Loosen validation on submission only when the PEC is deferred, which
    // allows for submissions regardless of toggle state that is not
    // persisted on refresh or re-authentication
    if (pecDeferred) {
      pecControl.clearValidators();
      pecControl.updateValueAndValidity();
    }

    const businessLicenceExpiryControl = this.businessLicenceFormState.businessLicenceExpiry;
    const businessLicenceExpiryDeferred = !businessLicenceExpiryControl.value;
    const doingBusinessAsControl = this.businessLicenceFormState.doingBusinessAs;
    const doingBusinessAsDeferred = !doingBusinessAsControl.value;

    // Expiry date is required, but sites exist that pre-date it's addition
    // to the application which need to have validation loosened
    if (businessLicenceExpiryDeferred) {
      businessLicenceExpiryControl.clearValidators();
      businessLicenceExpiryControl.updateValueAndValidity();

      if (isCPorPCHP && doingBusinessAsDeferred) {
        doingBusinessAsControl.clearValidators();
        doingBusinessAsControl.updateValueAndValidity();
      }
    }

    // Hours of operation disables fields when marked as 24 hours, which
    // persists to the overview during routing and needs to be enabled
    // to allow submission
    if (this.hoursOperationPageFormState.form.disabled) {
      this.hoursOperationPageFormState.form.enable();
    }

    // Assumption that PEC is required except for private community
    // health practice, and at this point can be considered deferred
    // when found to be empty
    if (
      !this.businessLicenceFormState.pec.value &&
      this.site.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
    ) {
      this.businessLicenceFormState.pec.disable();
    }

    const isSubmissionValid = this.isValid;

    // Re-apply the validations to prevent the validations being
    // incorrect if the page is visited after a submission fails
    if (pecDeferred) {
      pecControl.setValidators([Validators.required]);
      pecControl.updateValueAndValidity();
    }
    if (businessLicenceExpiryDeferred) {
      businessLicenceExpiryControl.setValidators([Validators.required]);
      businessLicenceExpiryControl.updateValueAndValidity();

      if (isCPorPCHP && doingBusinessAsDeferred) {
        doingBusinessAsControl.setValidators([Validators.required]);
        doingBusinessAsControl.updateValueAndValidity();
      }
    }

    return isSubmissionValid;
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  protected buildForms(): void {
    this.careSettingPageFormState = new CareSettingPageFormState(this.fb);
    this.businessLicenceFormState = new BusinessLicenceFormState(this.fb, this.siteResource, this.formUtilsService, this.siteService);
    this.hoursOperationPageFormState = new HoursOperationPageFormState(this.fb);
    this.remoteUsersPageFormState = new RemoteUsersPageFormState(this.fb);
    this.administratorPharmaNetFormState = new AdministratorPageFormState(this.fb, this.formUtilsService);
    this.privacyOfficerFormState = new PrivacyOfficerPageFormState(this.fb, this.formUtilsService);
    this.technicalSupportFormState = new TechnicalSupportPageFormState(this.fb, this.formUtilsService);
    this.deviceProviderFormState = new DeviceProviderPageFormState(this.fb);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(site: Site): void {
    if (!site) {
      return;
    }

    const { id, careSettingCode, siteVendors, doingBusinessAs, pec, businessLicence, activeBeforeRegistration, physicalAddress, isNew, deviceProviderId } = site;

    this.careSettingPageFormState.patchValue({ careSettingCode, siteVendors });
    this.businessLicenceFormState.patchValue({ doingBusinessAs, pec, businessLicence, activeBeforeRegistration, physicalAddress, isNew, careSettingCode, deviceProviderId }, id);
    this.hoursOperationPageFormState.patchValue(site?.businessHours);
    this.remoteUsersPageFormState.patchValue(site?.remoteUsers);
    this.administratorPharmaNetFormState.patchValue(site?.administratorPharmaNet);
    this.privacyOfficerFormState.patchValue(site?.privacyOfficer);
    this.technicalSupportFormState.patchValue(site?.technicalSupport);
    this.deviceProviderFormState.patchValue(site?.individualDeviceProviders);
  }
}
