import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl, FormControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ConfigService } from '@config/config.service';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
// TODO business models to shared or lib so there's no dependencies between feature modules
import { Site } from '@registration/shared/models/site.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';

import { RegulatoryFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { BcscDemographicFormState } from '@enrolment/pages/bcsc-demographic/bcsc-demographic-form-state.class';
import { BceidDemographicFormState } from '@enrolment/pages/bceid-demographic/bceid-demographic-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentFormStateService extends AbstractFormStateService<Enrolment> {
  public accessForm: FormGroup;
  public identityDocumentForm: FormGroup;
  public bceidDemographicFormState: BceidDemographicFormState;
  public bcscDemographicFormState: BcscDemographicFormState;
  public regulatoryFormState: RegulatoryFormState;
  public deviceProviderForm: FormGroup;
  public jobsForm: FormGroup;
  public remoteAccessForm: FormGroup;
  public remoteAccessLocationsForm: FormGroup;
  public selfDeclarationForm: FormGroup;
  public careSettingsForm: FormGroup;
  public accessAgreementForm: FormGroup;

  private identityProvider: IdentityProviderEnum;
  private enrolleeId: number;
  private userId: string;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    protected formUtilsService: FormUtilsService,
    private authService: AuthService,
    private configService: ConfigService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([...EnrolmentRoutes.enrolmentProfileRoutes()]);
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   *
   * NOTE: Executed by views to populate their form models, which
   * allows for it to be used for setting required values that
   * can't be loaded during instantiation.
   */
  public async setForm(enrolment: Enrolment, forcePatch: boolean = false): Promise<void> {
    if (!enrolment) {
      return;
    }

    // Must delay loading of the identity provider otherwise race
    // conditions occur when views initialize and the form instances
    // are not necessarily available
    this.identityProvider = await this.authService.identityProvider();

    // Store required enrolment identifiers not captured in forms
    this.enrolleeId = enrolment?.id;
    this.userId = enrolment?.enrollee.userId;

    super.setForm(enrolment, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Enrolment {
    const id = this.enrolleeId;
    const userId = this.userId;
    const profile = (this.identityProvider === IdentityProviderEnum.BCEID)
      ? this.bceidDemographicFormState.json
      : this.bcscDemographicFormState.json;
    const certifications = this.regulatoryFormState.json;
    const deviceProvider = this.deviceProviderForm.getRawValue();
    const { oboSites } = this.jobsForm.getRawValue();
    const { enrolleeRemoteUsers } = this.remoteAccessForm.getRawValue();
    const remoteAccessLocations = this.remoteAccessLocationsForm.getRawValue();
    const careSettings = this.convertCareSettingFormToJson(id);
    const selfDeclarations = this.convertSelfDeclarationsToJson();
    const remoteAccessSites = this.convertRemoteAccessSitesToJson();
    const { accessAgreementGuid } = this.accessAgreementForm.getRawValue();

    return {
      id,
      enrollee: {
        userId,
        ...profile
      },
      certifications,
      ...deviceProvider,
      oboSites,
      ...careSettings,
      enrolleeRemoteUsers,
      remoteAccessSites,
      ...remoteAccessLocations,
      selfDeclarations,
      accessAgreementGuid
    };
  }

  /**
   * @description
   * Helper for getting a list of enrolment forms.
   */
  public get forms(): AbstractControl[] {
    return [
      ...ArrayUtils.insertIf(
        this.identityProvider === IdentityProviderEnum.BCEID,
        // Purposefully omitted accessForm and identityDocumentForm
        // from the list of forms since they are used out of band
        this.bceidDemographicFormState.form
      ),
      ...ArrayUtils.insertIf(
        this.identityProvider === IdentityProviderEnum.BCSC,
        this.bcscDemographicFormState.form
      ),
      this.regulatoryFormState.form,
      // TODO commented out until required to avoid it being validated
      // this.deviceProviderForm,
      this.jobsForm,
      this.remoteAccessLocationsForm,
      this.selfDeclarationForm,
      this.careSettingsForm
    ];
  }

  /**
   * @description
   * Check that all constituent forms are valid.
   */
  public get isValid(): boolean {
    return super.isValid && this.hasCertificateOrJob();
  }

  /**
   * @description
   * Check for the requirement of at least one certification, or one obo site/job.
   */
  public hasCertificateOrJob(): boolean {
    // const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities').value as boolean[];
    // const oboSites = this.oboSitesForm.get('oboSites').value as OboSite[];
    // // When you set certifications to 'None' there still exists an item in
    // // the FormArray, and this checks for its existence
    // const hasCertification = !!this.regulatoryFormState.json.shift()?.licenseNumber;
    // const hasOboSiteForEveryChosenHA = enrolleeHealthAuthorities.every((checkbox: boolean) => {
    //   if (!checkbox) {
    //     return true;
    //   }
    //   // Only chosen health authorities need to be checked
    //   return oboSites.every((oboSite: OboSite, index: number) =>
    //     oboSite.healthAuthorityCode === this.configService.healthAuthorities[index].code
    //   );
    // });
    //
    // return hasCertification || hasOboSiteForEveryChosenHA;

    const oboSites = this.jobsForm.get('oboSites') as FormArray;
    const certifications = this.regulatoryFormState.certifications;
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities') as FormArray;
    let hasOboSiteForEveryHA = true;
    // Using `for` loop rather than `every()` method for ease of debugging
    for (let i = 0; i < enrolleeHealthAuthorities.controls.length; i++) {
      const checkbox = enrolleeHealthAuthorities.controls[i];
      // For every selected Health Authority ...
      if (checkbox.value === true) {
        let foundMatchingHAOboSite = false;
        // ... there must be at least one Obo Site for that Health Authority
        for (let j = 0; j < oboSites.controls.length; j++) {
          const oboSiteForm = oboSites.controls[j] as FormGroup;
          if (oboSiteForm.controls.healthAuthorityCode.value === this.configService.healthAuthorities[i].code) {
            foundMatchingHAOboSite = true;
            break;
          }
        }
        if (!foundMatchingHAOboSite) {
          hasOboSiteForEveryHA = false;
          break;
        }
      }
    }
    // When you set certifications to 'None' there still exists an item in
    // the FormArray, and this checks for its existence
    return (oboSites.length && hasOboSiteForEveryHA) || (certifications.length && certifications.value[0].licenseNumber);
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  protected buildForms() {
    // The accessForm and identityDocumentForm are used out of band
    // compared to the other form groups
    this.accessForm = this.buildAccessForm();
    this.identityDocumentForm = this.buildIdentityDocumentForm();

    this.bceidDemographicFormState = new BceidDemographicFormState(this.fb, this.formUtilsService);
    this.bcscDemographicFormState = new BcscDemographicFormState(this.fb, this.formUtilsService);
    this.regulatoryFormState = new RegulatoryFormState(this.fb);
    this.deviceProviderForm = this.buildDeviceProviderForm();
    this.jobsForm = this.buildJobsForm();
    this.remoteAccessForm = this.buildRemoteAccessForm();
    this.remoteAccessLocationsForm = this.buildRemoteAccessLocationsForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.careSettingsForm = this.buildCareSettingsForm();
    this.accessAgreementForm = this.buildAccessAgreementForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(enrolment: Enrolment) {
    if (!enrolment) {
      return;
    }

    (this.identityProvider === IdentityProviderEnum.BCEID)
      ? this.bceidDemographicFormState.patchValue(enrolment.enrollee)
      : this.bcscDemographicFormState.patchValue(enrolment.enrollee);
    this.deviceProviderForm.patchValue(enrolment);

    if (enrolment.careSettings.length) {
      const careSettings = this.careSettingsForm.get('careSettings') as FormArray;
      careSettings.clear();
      enrolment.careSettings.forEach((s: CareSetting) => {
        const careSetting = this.buildCareSettingForm();
        careSetting.patchValue(s);
        careSettings.push(careSetting);
      });
    }

    // Initialize Health Authority form even if it might not be used by end user:
    // Create checkboxes for each known Health Authority, according to order of Health Authority list.
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities') as FormArray;
    enrolleeHealthAuthorities.clear();
    // Set value of checkboxes according to previous selections, if any
    this.configService.healthAuthorities.forEach(ha => {
      const checked = enrolment.enrolleeHealthAuthorities.some(eha => ha.code === eha.healthAuthorityCode);
      enrolleeHealthAuthorities.push(this.buildEnrolleeHealthAuthorityFormControl(checked));
    });

    this.careSettingsForm.get('careSettings').patchValue(enrolment.careSettings);

    if (enrolment.oboSites.length) {
      const oboSites = this.jobsForm.get('oboSites') as FormArray;
      const communityHealthSites = this.jobsForm.get('communityHealthSites') as FormArray;
      const communityPharmacySites = this.jobsForm.get('communityPharmacySites') as FormArray;
      const healthAuthoritySites = this.jobsForm.get('healthAuthoritySites') as FormGroup;

      oboSites.clear();
      communityHealthSites.clear();
      communityPharmacySites.clear();
      Object.keys(healthAuthoritySites.controls).forEach(healthAuthorityCode => healthAuthoritySites.removeControl(healthAuthorityCode));

      enrolment.oboSites.forEach((s: OboSite) => {
        const site = this.buildOboSiteForm();
        site.patchValue(s);
        oboSites.push(site);

        switch (s.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
            this.addNonHealthAuthorityOboSite(site, communityHealthSites);
            break;
          }
          case CareSettingEnum.COMMUNITY_PHARMACIST: {
            this.addNonHealthAuthorityOboSite(site, communityPharmacySites);
            break;
          }
          case CareSettingEnum.HEALTH_AUTHORITY: {
            this.addHealthAuthorityOboSite(site, healthAuthoritySites, s.healthAuthorityCode);
            break;
          }
        }
      });
    }

    if (enrolment.enrolleeRemoteUsers.length) {
      const enrolleeRemoteUsers = this.remoteAccessForm.get('enrolleeRemoteUsers') as FormArray;
      enrolleeRemoteUsers.clear();
      enrolment.enrolleeRemoteUsers.forEach((eru: EnrolleeRemoteUser) => {
        const enrolleeRemoteUser = this.enrolleeRemoteUserFormGroup();
        enrolleeRemoteUser.patchValue(eru);
        enrolleeRemoteUsers.push(enrolleeRemoteUser);
      });
    }

    if (enrolment.remoteAccessSites.length) {
      const remoteAccessSites = this.remoteAccessForm.get('remoteAccessSites') as FormArray;
      remoteAccessSites.clear();
      enrolment.remoteAccessSites.forEach((ras: RemoteAccessSite) => {
        const remoteAccessSite = this.remoteAccessSiteFormGroup();
        // Add the vendors, and then patch the remaining fields
        const siteVendors = remoteAccessSite.get('siteVendors') as FormArray;
        ras.site.siteVendors
          .forEach(v => siteVendors.push(this.fb.group({ vendorCode: v.vendorCode })));

        remoteAccessSite.patchValue({
          enrolleeId: ras.enrolleeId,
          siteId: ras.siteId,
          doingBusinessAs: ras.site.doingBusinessAs
        });
        remoteAccessSites.push(remoteAccessSite);
      });
    }

    if (enrolment.remoteAccessLocations.length) {
      const remoteAccessLocations = this.remoteAccessLocationsForm.get('remoteAccessLocations') as FormArray;
      remoteAccessLocations.clear();
      enrolment.remoteAccessLocations.forEach((ral: RemoteAccessLocation) => {
        const remoteAccessLocation = this.remoteAccessLocationFormGroup();
        remoteAccessLocation.patchValue(ral);
        remoteAccessLocations.push(remoteAccessLocation);
      });
    }
    this.regulatoryFormState.patchValue(enrolment.certifications);
    this.jobsForm.patchValue(enrolment);
    this.remoteAccessForm.patchValue(enrolment);
    this.remoteAccessLocationsForm.patchValue(enrolment);

    const defaultValue = (enrolment.profileCompleted) ? false : null;
    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED
    };
    const selfDeclarations = Object.keys(selfDeclarationsTypes)
      .reduce((sds, sd) => {
        const type = selfDeclarationsTypes[sd];
        const selfDeclarationDetails = enrolment.selfDeclarations
          .find(esd => esd.selfDeclarationTypeCode === type)
          ?.selfDeclarationDetails;
        const adapted = {
          [sd]: (selfDeclarationDetails) ? true : defaultValue,
          [`${sd}Details`]: (selfDeclarationDetails) ? selfDeclarationDetails : null
        };
        return { ...sds, ...adapted };
      }, {});

    this.selfDeclarationForm.patchValue(selfDeclarations);

    // After patching the form is dirty, and needs to be pristine
    // to allow for deactivation modals to work properly
    this.markAsPristine();
  }

  /**
   * @description
   * Determine whether the form should be reset based
   * on the current route path.
   */
  protected checkResetRoutes(currentRoutePath: string, resetRoutes: string[]): boolean {
    return !resetRoutes?.includes(currentRoutePath);
  }

  /**
   * JSON Helpers
   */

  private convertSelfDeclarationsToJson(): SelfDeclaration[] {
    const selfDeclarations = this.selfDeclarationForm.getRawValue();
    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED
    };
    return Object.keys(selfDeclarationsTypes)
      .reduce((sds: SelfDeclaration[], sd: string) => {
        if (selfDeclarations[sd]) {
          sds.push(
            new SelfDeclaration(
              selfDeclarationsTypes[sd],
              selfDeclarations[`${sd}Details`],
              selfDeclarations[`${sd}DocumentGuids`],
              this.enrolleeId
            )
          );
        }
        return sds;
      }, []);
  }

  private convertRemoteAccessSitesToJson(): RemoteAccessSite[] {
    const { remoteAccessSites } = this.remoteAccessForm.getRawValue();

    return remoteAccessSites.map((ras) => {
      return {
        enrolleeId: ras.enrolleeId,
        siteId: ras.siteId,
        site: {
          doingBusinessAs: ras.doingBusinessAs,
          physicalAddress: ras.physicalAddress,
          siteVendors: ras.siteVendors
        } as Site,
      } as RemoteAccessSite;
    });
  }

  private convertCareSettingFormToJson(enrolleeId: number): any {
    // Variable names must match keys for FormArrays in the FormGroup to get values
    // tslint:disable-next-line:prefer-const
    let { careSettings, enrolleeHealthAuthorities } = this.careSettingsForm.getRawValue();

    // Any checked HA is converted into an enrollee health authority object literal,
    // which is used to create the payload to back-end
    enrolleeHealthAuthorities = enrolleeHealthAuthorities.reduce((selectedHealthAuthorities, checked, i) => {
      if (checked) {
        selectedHealthAuthorities.push({
          enrolleeId,
          healthAuthorityCode: this.configService.healthAuthorities[i].code
        });
      }
      return selectedHealthAuthorities;
    }, []);
    return { careSettings, enrolleeHealthAuthorities };
  }

  /**
   * Form Builders and Helpers
   */

  private buildAccessForm(): FormGroup {
    return this.fb.group({
      accessCode: ['', [
        Validators.required,
        Validators.pattern(/^lobster$/)
      ]]
    });
  }

  private buildIdentityDocumentForm(): FormGroup {
    return this.fb.group({
      identificationDocumentGuid: [null, [Validators.required]]
    });
  }

  public buildJobsForm(): FormGroup {
    return this.fb.group({
      oboSites: this.fb.array([]),
      communityHealthSites: this.fb.array([]),
      communityPharmacySites: this.fb.array([]),
      healthAuthoritySites: this.fb.group({})
    });
  }

  public buildOboSiteForm(): FormGroup {
    return this.fb.group({
      careSettingCode: [null, []],
      healthAuthorityCode: [null, []],
      siteName: [null, []],
      facilityName: [null, []],
      jobTitle: [null, [Validators.required]],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2'],
        useDefaults: ['provinceCode', 'countryCode'],
        areDisabled: ['provinceCode', 'countryCode']
      }),
      pec: [null, []]
    });
  }

  public buildDeviceProviderForm(): FormGroup {
    return this.fb.group({
      deviceProviderNumber: [null, [
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]],
      isInsulinPumpProvider: [false, [FormControlValidators.requiredBoolean]]
    });
  }

  public buildRemoteAccessForm(): FormGroup {
    return this.fb.group({
      remoteAccessSites: this.fb.array([]),
      enrolleeRemoteUsers: this.fb.array([])
    });
  }

  public enrolleeRemoteUserFormGroup(): FormGroup {
    return this.fb.group({
      enrolleeId: [null, []],
      remoteUserId: [null, []]
    });
  }

  public remoteAccessSiteFormGroup(): FormGroup {
    return this.fb.group({
      enrolleeId: [null, []],
      siteId: [null, []],
      doingBusinessAs: [null, []],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2']
      }),
      siteVendors: this.fb.array([])
    });
  }

  public buildRemoteAccessLocationsForm(): FormGroup {
    return this.fb.group({
      remoteAccessLocations: this.fb.array([])
    });
  }

  public remoteAccessLocationFormGroup(): FormGroup {
    return this.fb.group({
      internetProvider: [
        null,
        [Validators.required]
      ],
      physicalAddress: this.formUtilsService.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2'],
        useDefaults: ['provinceCode', 'countryCode'],
        areDisabled: ['provinceCode', 'countryCode']
      })
    });
  }

  private buildCareSettingsForm(): FormGroup {
    return this.fb.group({
      careSettings: this.fb.array([]),
      enrolleeHealthAuthorities: this.fb.array([])
    });
  }

  public buildCareSettingForm(code: number = null): FormGroup {
    return this.fb.group({
      careSettingCode: [code, [Validators.required]]
    });
  }

  public buildEnrolleeHealthAuthorityFormControl(checkState: boolean): FormControl {
    return this.fb.control(checkState);
  }

  public removeHealthAuthorities() {
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities') as FormArray;
    enrolleeHealthAuthorities.controls.forEach(checkbox => {
      checkbox.setValue(false);
    });
  }

  private buildSelfDeclarationForm(): FormGroup {
    return this.fb.group({
      hasConviction: [null, [FormControlValidators.requiredBoolean]],
      hasConvictionDetails: [null, []],
      hasConvictionDocumentGuids: this.fb.array([]),
      hasRegistrationSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspendedDetails: [null, []],
      hasRegistrationSuspendedDocumentGuids: this.fb.array([]),
      hasDisciplinaryAction: [null, [FormControlValidators.requiredBoolean]],
      hasDisciplinaryActionDetails: [null, []],
      hasDisciplinaryActionDocumentGuids: this.fb.array([]),
      hasPharmaNetSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasPharmaNetSuspendedDetails: [null, []],
      hasPharmaNetSuspendedDocumentGuids: this.fb.array([])
    });
  }

  private buildAccessAgreementForm(): FormGroup {
    return this.fb.group({
      accessAgreementGuid: [
        '',
        []
      ]
    });
  }

  public addNonHealthAuthorityOboSite(siteForm: FormGroup, siteFormList: FormArray) {
    const siteName = siteForm.get('siteName') as FormControl;
    this.formUtilsService.setValidators(siteName, [Validators.required]);
    siteFormList.push(siteForm);
  }

  /**
   * @param haSiteForm - aka Health Authority Facility Form
   * @param healthAuthoritySites - a FormArray where each element, representing a Health Authority where the enrollee
   * works, contains a FormArray. This nested FormArray contains a FormGroup for each facility that the enrollee works
   * at, in that Health Authority
   */
  public addHealthAuthorityOboSite(haSiteForm: FormGroup, healthAuthoritySites: FormGroup, healthAuthorityCode: number) {
    const facilityName = haSiteForm.get('facilityName') as FormControl;
    this.formUtilsService.setValidators(facilityName, [Validators.required]);
    let sitesOfHealthAuthority = healthAuthoritySites.get(String(healthAuthorityCode)) as FormArray;
    if (!sitesOfHealthAuthority) {
      sitesOfHealthAuthority = this.fb.array([]);
      sitesOfHealthAuthority.setValidators([FormArrayValidators.atLeast(1)]);
      healthAuthoritySites.setControl(String(healthAuthorityCode), sitesOfHealthAuthority);
    }
    sitesOfHealthAuthority.push(haSiteForm);
  }

  public removeUnselectedHAOboSites() {
    // Obo Sites need to be removed from two different collections
    const oboSites = this.jobsForm.get('oboSites') as FormArray;
    const healthAuthoritySites = this.jobsForm.get('healthAuthoritySites') as FormGroup;
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities') as FormArray;
    // If the checkbox for the health authority is not selected, remove the corresponding Obo Sites
    this.configService.healthAuthorities.forEach((healthAuthority, index) => {
      if (!enrolleeHealthAuthorities.at(index).value) {
        for (let i = oboSites.controls.length - 1; i >= 0; i--) {
          const oboSiteForm = oboSites.controls[i] as FormGroup;
          if (oboSiteForm.controls.healthAuthorityCode.value === healthAuthority.code) {
            oboSites.removeAt(i);
          }
        }
        healthAuthoritySites.removeControl(String(healthAuthority.code));
      }
    });
  }

  /**
   * Document Upload Helpers
   */

  public addSelfDeclarationDocumentGuid(control: FormArray, value: string) {
    control.push(this.fb.control(value));
  }

  public removeSelfDeclarationDocumentGuid(control: FormArray, documentGuid: string) {
    control.removeAt(control.value.findIndex((guid: string) => guid === documentGuid));
  }

  public clearSelfDeclarationDocumentGuids() {
    [
      'hasConvictionDocumentGuids',
      'hasRegistrationSuspendedDocumentGuids',
      'hasDisciplinaryActionDocumentGuids',
      'hasPharmaNetSuspendedDocumentGuids'
    ]
      .map((formArrayName: string) => this.selfDeclarationForm.get(formArrayName) as FormArray)
      .forEach((formArray: FormArray) => formArray.clear());
  }
}
