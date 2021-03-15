import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { RouteStateService } from '@core/services/route-state.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { Contact } from '@registration/shared/models/contact.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { RemoteUserCertification } from '@registration/shared/models/remote-user-certification.model';
import { SiteAddressPageFormState } from '@registration/pages/site-address-page/site-address-page-form-state.class';
import { HoursOperationPageFormState } from '@registration/pages/hours-operation-page/hours-operation-page-form-state.class';
import { AdministratorFormState } from '@registration/pages/administrator/administrator-form-state.class';
import { PrivacyOfficerFormState } from '@registration/pages/privacy-officer/privacy-officer-form-state.class';
import { TechnicalSupportFormState } from '@registration/pages/technical-support/technical-support-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class SiteFormStateService extends AbstractFormStateService<Site> {
  public careSettingTypeForm: FormGroup;
  public businessForm: FormGroup;
  public siteAddressPageFormState: SiteAddressPageFormState;
  public hoursOperationPageFormState: HoursOperationPageFormState;
  public remoteUsersForm: FormGroup;
  public administratorPharmaNetFormState: AdministratorFormState;
  public privacyOfficerFormState: PrivacyOfficerFormState;
  public technicalSupportFormState: TechnicalSupportFormState;

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
    const remoteUsers = this.remoteUsersForm.getRawValue().remoteUsers
      .map((ru: RemoteUser) => {
        // Remove the ID from the remote user to simplify updates on the server
        const { id, ...remoteUser } = ru;
        return remoteUser;
      });
    const [administratorPharmaNet, privacyOfficer, technicalSupport] = [
      this.administratorPharmaNetFormState.json,
      this.privacyOfficerFormState.json,
      this.technicalSupportFormState.json
    ].map((contact: Contact) => this.toPersonJson<Contact>(contact));

    // Includes site related keys to uphold relationships, and allow for updates
    // to a site. Keys not for update have been omitted and the type enforced
    return {
      id: this.siteId,
      organizationId: this.organizationId,
      // organization (N/A)
      provisionerId: this.provisionerId,
      // provisioner (N/A)
      careSettingCode,
      // Only using single vendors for now
      siteVendors: [{
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
      this.remoteUsersForm,
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
    this.remoteUsersForm = this.buildRemoteUsersForm();
    this.administratorPharmaNetFormState = new AdministratorFormState(this.fb, this.formUtilsService);
    this.privacyOfficerFormState = new PrivacyOfficerFormState(this.fb, this.formUtilsService);
    this.technicalSupportFormState = new TechnicalSupportFormState(this.fb, this.formUtilsService);
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

    const remoteUsersForm = this.remoteUsersForm;
    const remoteUsersFormArray = remoteUsersForm.get('remoteUsers') as FormArray;
    remoteUsersFormArray.clear(); // Clear out existing indices
    if (site.remoteUsers?.length) {
      // Omitted from payload, but provided in the form to allow for
      // validation to occur when "Have Remote Users" is toggled
      remoteUsersForm.get('hasRemoteUsers').patchValue(!!site.remoteUsers.length);

      site.remoteUsers.forEach((remoteUser: RemoteUser) => {
        const group = this.createEmptyRemoteUserFormAndPatch(remoteUser);
        remoteUsersFormArray.push(group);
      });
    }

    [
      [this.administratorPharmaNetFormState.form, site.administratorPharmaNet],
      [this.privacyOfficerFormState.form, site.privacyOfficer],
      [this.technicalSupportFormState.form, site.technicalSupport]
    ]
      .filter(([form, data]: [FormGroup, Party]) => data)
      .forEach((formParty: [FormGroup, Party]) => this.toPersonFormModel<Contact>(formParty));
  }

  /**
   * Form Builders and Helpers
   */

  /**
   * @description
   * Create an empty remote user form group, and patch
   * it with a remote user if provided.
   */
  public createEmptyRemoteUserFormAndPatch(remoteUser: RemoteUser = null): FormGroup {
    const group = this.remoteUserFormGroup();
    if (remoteUser) {
      const { id, firstName, lastName, email, remoteUserCertifications } = remoteUser;
      group.patchValue({ id, firstName, lastName, email });

      const certs = group.get('remoteUserCertifications') as FormArray;
      remoteUserCertifications.map((cert: RemoteUserCertification) => {
        const formGroup = this.remoteUserCertificationFormGroup();
        formGroup.patchValue(cert);
        return formGroup;
      }).forEach((remoteUserCertificationFormGroup: FormGroup) =>
        certs.push(remoteUserCertificationFormGroup)
      );
    }

    return group;
  }

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

  private buildRemoteUsersForm(): FormGroup {
    return this.fb.group({
      // Omitted from payload, but provided in the form to allow for
      // validation to occur when "Have Remote Users" is toggled
      hasRemoteUsers: [
        false,
        []
      ],
      remoteUsers: this.fb.array(
        [],
        []
      )
      // TODO at least one remote users is required
      // [FormArrayValidators.atLeast(1)]
    });
  }

  private remoteUserFormGroup(): FormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        null,
        [Validators.required]
      ],
      lastName: [
        null,
        [Validators.required]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      remoteUserCertifications: this.fb.array(
        [],
        { validators: FormArrayValidators.atLeast(1) }
      )
    });
  }

  public remoteUserCertificationFormGroup(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', []],
      // Validators are applied at the component-level when
      // fields are made visible to allow empty submissions
      licenseNumber: [null, []],
      licenseCode: [null, []]
    });
  }
}
