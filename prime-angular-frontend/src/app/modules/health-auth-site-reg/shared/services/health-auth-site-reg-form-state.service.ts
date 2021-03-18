import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { Site } from '@registration/shared/models/site.model';

import { HealthAuthSiteRegRoutes } from '../../health-auth-site-reg.routes';
import { CareSettingPageFormState } from '../../pages/care-setting/care-setting-page-form-state.class';
import { VendorPageFormState } from '../../pages/vendor/vendor-page-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthSiteRegFormStateService extends AbstractFormStateService<Site> {
  public vendorPageFormState: VendorPageFormState;
  public careSettingPageFormState: CareSettingPageFormState;

  private siteId: number;
  private organizationId: number;
  private provisionerId: number;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    private formUtilsService: FormUtilsService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([HealthAuthSiteRegRoutes.SITE_MANAGEMENT]);
  }

  public get json(): Site {
    const { siteVendors } = this.vendorPageFormState.json;
    const { careSettingCode } = this.careSettingPageFormState.json;
    // const { businessLicenceGuid, doingBusinessAs, deferredLicenceReason, pec } = this.businessLicencePageFormState.json;
    // const physicalAddress = this.siteAddressPageFormState.json;
    // const businessHours = this.hoursOperationPageFormState.json;
    // const remoteUsers = this.remoteUsersPageFormState.json;
    // const administratorPharmaNet = this.administratorPharmaNetFormState.json;
    // const privacyOfficer = this.privacyOfficerFormState.json;
    // const technicalSupport = this.technicalSupportFormState.json;

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
    } as Site; // Enforced type due to N/A properties
  }

  public get forms(): AbstractControl[] {
    return [
      this.vendorPageFormState.form,
      this.careSettingPageFormState.form,
      // this.businessLicencePageFormState.form,
      // this.siteAddressPageFormState.form,
      // this.hoursOperationPageFormState.form,
      // this.remoteUsersPageFormState.form,
      // this.administratorPharmaNetFormState.form,
      // this.privacyOfficerFormState.form,
      // this.technicalSupportFormState.form
    ];
  }

  protected buildForms(): void {
    this.vendorPageFormState = new VendorPageFormState(this.fb);
    this.careSettingPageFormState = new CareSettingPageFormState(this.fb);
  }

  protected patchForm(site: Site): void {
    if (!site) {
      return;
    }

    const { siteVendors, careSettingCode, pec, businessLicenceGuid, deferredLicenceReason, doingBusinessAs } = site;

    this.vendorPageFormState.patchValue({ siteVendors });
    this.careSettingPageFormState.patchValue({ careSettingCode });
    // this.businessLicencePageFormState.patchValue({ businessLicenceGuid, deferredLicenceReason, doingBusinessAs, pec });
    // this.siteAddressPageFormState.patchValue(site?.physicalAddress);
    // this.hoursOperationPageFormState.patchValue(site?.businessHours);
    // this.remoteUsersPageFormState.patchValue(site?.remoteUsers);
    // this.administratorPharmaNetFormState.patchValue(site?.administratorPharmaNet);
    // this.privacyOfficerFormState.patchValue(site?.privacyOfficer);
    // this.technicalSupportFormState.patchValue(site?.technicalSupport);
  }
}
