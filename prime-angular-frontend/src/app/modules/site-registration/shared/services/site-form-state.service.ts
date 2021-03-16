import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { Contact } from '@lib/models/contact.model';
import { SiteAddressPageFormState } from '@registration/pages/site-address-page/site-address-page-form-state.class';
import { HoursOperationPageFormState } from '@registration/pages/hours-operation-page/hours-operation-page-form-state.class';
import { AdministratorFormState } from '@registration/pages/administrator-page/administrator-page-form-state.class';
import { PrivacyOfficerPageFormState } from '@registration/pages/privacy-officer-page/privacy-officer-page-form-state.class';
import { TechnicalSupportPageFormState } from '@registration/pages/technical-support-page/technical-support-page-form-state.class';
import { RemoteUsersPageFormState } from '@registration/pages/remote-users-page/remote-users-page-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class SiteFormStateService extends AbstractFormStateService<Site> {
  public careSettingTypeForm: FormGroup;
  public businessForm: FormGroup;
  public siteAddressPageFormState: SiteAddressPageFormState;
  public hoursOperationPageFormState: HoursOperationPageFormState;
  public remoteUsersPageFormState: RemoteUsersPageFormState;
  public administratorPharmaNetFormState: AdministratorFormState;
  public privacyOfficerFormState: PrivacyOfficerPageFormState;
  public technicalSupportFormState: TechnicalSupportPageFormState;

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

    this.initialize([SiteRoutes.SITE_MANAGEMENT]);
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(site: Site, forcePatch: boolean = false) {
    if (!site) {
      return;
    }

    // Store required site identifiers not captured in forms
    this.siteId = site.id;
    this.organizationId = site.organizationId;
    this.provisionerId = site.provisionerId;

    super.setForm(site, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Site {
    const { careSettingCode, vendorCode, pec } = this.careSettingTypeForm.getRawValue();
    const { businessLicenceGuid, doingBusinessAs, deferredLicenceReason } = this.businessForm.getRawValue();
    const physicalAddress = this.siteAddressPageFormState.json;
    const businessHours = this.hoursOperationPageFormState.json;
    const remoteUsers = this.remoteUsersPageFormState.json;
    const [administratorPharmaNet, privacyOfficer, technicalSupport] = [
      this.administratorPharmaNetFormState.json,
      this.privacyOfficerFormState.json,
      this.technicalSupportFormState.json
    ].map((contact: Contact) => this.formUtilsService.toPersonJson<Contact>(contact));

    // Includes site related keys to uphold relationships, and allow for updates
    // to a site. Keys not for update have been omitted and the type enforced
    return {
      id: this.siteId,
      organizationId: this.organizationId,
      // organization (N/A)
      provisionerId: this.provisionerId,
      // provisioner (N/A)
      careSettingCode,
      siteVendors: [{ // TODO only use a single vendor, should look at dropping vendors for vendor
        siteId: this.siteId,
        vendorCode
      }],
      businessLicenceGuid,
      deferredLicenceReason,
      doingBusinessAs,
      physicalAddressId: physicalAddress?.id, // TODO can this be dropped?
      physicalAddress,
      businessHours,
      remoteUsers,
      administratorPharmaNetId: administratorPharmaNet?.id,
      administratorPharmaNet,
      privacyOfficerId: privacyOfficer?.id,
      privacyOfficer,
      technicalSupportId: technicalSupport?.id,
      technicalSupport,
      // completed (N/A)
      // approvedDate (N/A)
      // submittedDate (N/A)
      pec
    } as Site; // Enforced type
  }

  /**
   * @description
   * Helper for getting a list of organization forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.careSettingTypeForm,
      this.businessForm,
      this.siteAddressPageFormState.form,
      this.hoursOperationPageFormState.form,
      this.remoteUsersPageFormState.form,
      this.administratorPharmaNetFormState.form,
      this.privacyOfficerFormState.form,
      this.technicalSupportFormState.form
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  protected buildForms() {
    this.careSettingTypeForm = this.buildCareSettingTypeForm();
    this.businessForm = this.buildBusinessForm();
    this.siteAddressPageFormState = new SiteAddressPageFormState(this.fb, this.formUtilsService);
    this.hoursOperationPageFormState = new HoursOperationPageFormState(this.fb);
    this.remoteUsersPageFormState = new RemoteUsersPageFormState(this.fb);
    this.administratorPharmaNetFormState = new AdministratorFormState(this.fb, this.formUtilsService);
    this.privacyOfficerFormState = new PrivacyOfficerPageFormState(this.fb, this.formUtilsService);
    this.technicalSupportFormState = new TechnicalSupportPageFormState(this.fb, this.formUtilsService);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(site: Site): void {
    if (!site) {
      return;
    }

    this.careSettingTypeForm.patchValue(site);

    if (site.siteVendors?.length) {
      this.careSettingTypeForm.get('vendorCode').patchValue(site.siteVendors[0].vendorCode);
    }

    if (site.doingBusinessAs) {
      this.businessForm.get('doingBusinessAs').patchValue(site.doingBusinessAs);
    }

    if (site.businessLicence) {
      this.businessForm.get('deferredLicenceReason').patchValue(site.businessLicence.deferredLicenceReason);
    }

    this.siteAddressPageFormState.patchValue(site?.physicalAddress);
    this.hoursOperationPageFormState.patchValue(site?.businessHours);
    this.remoteUsersPageFormState.patchValue(site?.remoteUsers);
    [
      [this.administratorPharmaNetFormState.form, site?.administratorPharmaNet],
      [this.privacyOfficerFormState.form, site?.privacyOfficer],
      [this.technicalSupportFormState.form, site?.technicalSupport]
    ]
      .filter(([_, data]: [FormGroup, Party]) => data)
      .forEach((form: [FormGroup, Party]) => this.formUtilsService.toPersonFormModel<Contact>(form));
  }

  /**
   * Form Builders and Helpers
   */

  private buildCareSettingTypeForm(code: number = null, pec: string = null): FormGroup {
    return this.fb.group({
      careSettingCode: [
        code,
        [Validators.required]
      ],
      vendorCode: [
        0,
        [FormControlValidators.requiredIndex]
      ],
      pec: [
        pec,
        [Validators.required]
      ]
    });
  }

  private buildBusinessForm(): FormGroup {
    return this.fb.group({
      businessLicenceGuid: [
        '',
        []
      ],
      deferredLicenceReason: [
        '',
        []
      ],
      doingBusinessAs: [
        '',
        [Validators.required]
      ]
    });
  }
}
