import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';

import { HealthAuthSite } from '@health-auth/shared/models/health-auth-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { SiteInformationPageFormState } from '@health-auth/pages/site-information-page/site-information-page-form-state.class';
import { HealthAuthCareSettingPageFormState } from '@health-auth/pages/health-auth-care-setting-page/health-auth-care-setting-page-form-state.class';
import { VendorPageFormState } from '@health-auth/pages/vendor-page/vendor-page-form-state.class';
import { AuthorizedUserPageFormState } from '@health-auth/pages/authorized-user-page/authorized-user-page-form-state.class';
import { SiteAddressPageFormState } from '@health-auth/pages/site-address-page/site-address-page-form-state.class';
import { HoursOperationPageFormState } from '@health-auth/pages/hours-operation-page/hours-operation-page-form-state.class';
import { RemoteUsersPageFormState } from '@health-auth/pages/remote-users-page/remote-users-page-form-state.class';
import { AdministratorPageFormState } from '@health-auth/pages/administrator-page/administrator-page-form-state.class';
import { PrivacyOfficerPageFormState } from '@health-auth/pages/privacy-officer-page/privacy-officer-page-form-state.class';
import { TechnicalSupportPageFormState } from '@health-auth/pages/technical-support-page/technical-support-page-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthSiteRegFormStateService extends AbstractFormStateService<HealthAuthSite> {
  public vendorPageFormState: VendorPageFormState;
  public healthAuthCareSettingPageFormState: HealthAuthCareSettingPageFormState;
  public siteInfoPageFormState: SiteInformationPageFormState;
  public siteAddressPageFormState: SiteAddressPageFormState;
  public hoursOperationPageFormState: HoursOperationPageFormState;
  public remoteUsersPageFormState: RemoteUsersPageFormState;
  public administratorPageFormState: AdministratorPageFormState;
  public privacyOfficerPageFormState: PrivacyOfficerPageFormState;
  public technicalSupportPageFormState: TechnicalSupportPageFormState;

  // private siteId: number;
  // private organizationId: number;
  // private provisionerId: number;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    private formUtilsService: FormUtilsService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([HealthAuthSiteRegRoutes.SITE_MANAGEMENT]);
  }

  public get json(): HealthAuthSite {
    const { siteVendors } = this.vendorPageFormState.json;
    const { careSettingCode } = this.healthAuthCareSettingPageFormState.json;
    const { doingBusinessAs, pec } = this.siteInfoPageFormState.json;
    const physicalAddress = this.siteAddressPageFormState.json;
    const businessHours = this.hoursOperationPageFormState.json;
    const remoteUsers = this.remoteUsersPageFormState.json;
    const administratorPharmaNet = this.administratorPageFormState.json;
    const privacyOfficer = this.privacyOfficerPageFormState.json;
    const technicalSupport = this.technicalSupportPageFormState.json;

    // Includes site related keys to uphold relationships, and allow for updates
    // to a site. Keys not for update have been omitted and the type enforced
    return {
      // id: this.siteId,
      // organizationId: this.organizationId,
      // organization (N/A)
      // provisionerId: this.provisionerId,
      // provisioner (N/A)
      // careSettingCode,
      // siteVendors,
      // businessLicenceGuid,
      // deferredLicenceReason,
      // doingBusinessAs,
      // physicalAddressId: physicalAddress?.id, // TODO can this be dropped?
      // physicalAddress,
      // businessHours,
      // remoteUsers,
      // administratorPharmaNetId: administratorPharmaNet?.id, // TODO can this be dropped?
      // administratorPharmaNet,
      // privacyOfficerId: privacyOfficer?.id, // TODO can this be dropped?
      // privacyOfficer,
      // technicalSupportId: technicalSupport?.id, // TODO can this be dropped?
      // technicalSupport,
      // completed (N/A)
      // approvedDate (N/A)
      // submittedDate (N/A)
      // pec
      // TODO output should be a Site-like model instead due to missing properties
    } as HealthAuthSite; // Enforced type due to N/A properties
  }

  public get forms(): AbstractControl[] {
    return [
      this.vendorPageFormState.form,
      this.healthAuthCareSettingPageFormState.form,
      this.siteInfoPageFormState.form,
      this.siteAddressPageFormState.form,
      this.hoursOperationPageFormState.form,
      this.remoteUsersPageFormState.form,
      this.administratorPageFormState.form,
      this.privacyOfficerPageFormState.form,
      this.technicalSupportPageFormState.form
    ];
  }

  protected buildForms(): void {
    this.vendorPageFormState = new VendorPageFormState(this.fb);
    this.healthAuthCareSettingPageFormState = new HealthAuthCareSettingPageFormState(this.fb);
    this.siteInfoPageFormState = new SiteInformationPageFormState(this.fb);
    this.siteAddressPageFormState = new SiteAddressPageFormState(this.fb, this.formUtilsService);
    this.hoursOperationPageFormState = new HoursOperationPageFormState(this.fb);
    this.remoteUsersPageFormState = new RemoteUsersPageFormState(this.fb);
    this.administratorPageFormState = new AdministratorPageFormState(this.fb, this.formUtilsService);
    this.privacyOfficerPageFormState = new PrivacyOfficerPageFormState(this.fb, this.formUtilsService);
    this.technicalSupportPageFormState = new TechnicalSupportPageFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(site: HealthAuthSite): void {
    if (!site) {
      return;
    }

    const { siteVendors, careSettingCode, doingBusinessAs, pec, } = site;

    this.vendorPageFormState.patchValue({ siteVendors });
    this.healthAuthCareSettingPageFormState.patchValue({ careSettingCode });
    this.siteInfoPageFormState.patchValue({ doingBusinessAs, pec });
    this.siteAddressPageFormState.patchValue(site?.physicalAddress);
    this.hoursOperationPageFormState.patchValue(site?.businessHours);
    this.remoteUsersPageFormState.patchValue(site?.remoteUsers);
    this.administratorPageFormState.patchValue(site?.administratorPharmaNet);
    this.privacyOfficerPageFormState.patchValue(site?.privacyOfficer);
    this.technicalSupportPageFormState.patchValue(site?.technicalSupport);
  }
}
