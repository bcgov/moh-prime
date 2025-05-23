import { Injectable } from '@angular/core';
import { UntypedFormBuilder, Validators, UntypedFormGroup, UntypedFormArray, AbstractControl, UntypedFormControl } from '@angular/forms';

import moment from 'moment';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ConfigService } from '@config/config.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
// TODO business models to shared or lib so there's no dependencies between feature modules
import { Site } from '@registration/shared/models/site.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';

import { BcscDemographicFormState } from '@enrolment/pages/bcsc-demographic/bcsc-demographic-form-state.class';
import { BceidDemographicFormState } from '@enrolment/pages/bceid-demographic/bceid-demographic-form-state.class';
import { RegulatoryFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { PaperEnrolleeReturneeFormState } from '@enrolment/pages/paper-enrollee-returnees-page/paper-enrollee-returnee-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentFormStateService extends AbstractFormStateService<Enrolment> {
  public accessForm: UntypedFormGroup;
  public identityDocumentForm: UntypedFormGroup;
  public paperEnrolleeReturneeFormState: PaperEnrolleeReturneeFormState;
  public bceidDemographicFormState: BceidDemographicFormState;
  public bcscDemographicFormState: BcscDemographicFormState;
  public regulatoryFormState: RegulatoryFormState;
  public deviceProviderForm: UntypedFormGroup;
  public oboSitesForm: UntypedFormGroup;
  public remoteAccessForm: UntypedFormGroup;
  public remoteAccessLocationsForm: UntypedFormGroup;
  public selfDeclarationForm: UntypedFormGroup;
  public careSettingsForm: UntypedFormGroup;
  public accessAgreementForm: UntypedFormGroup;

  public selfDeclarationCompletedDate: string;

  private identityProvider: IdentityProviderEnum;
  private enrolleeId: number;
  private userId: string;

  constructor(
    protected fb: UntypedFormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
    protected formUtilsService: FormUtilsService,
    protected enrolmentResource: EnrolmentResource,
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
    const paperProfile = this.paperEnrolleeReturneeFormState.json;
    const profile = (this.identityProvider === IdentityProviderEnum.BCEID)
      ? this.bceidDemographicFormState.json
      : this.bcscDemographicFormState.json;
    const regulatory = this.regulatoryFormState.json;
    const { oboSites } = this.oboSitesForm.getRawValue();
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
        ...profile,
        ...paperProfile
      },
      ...regulatory,
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
      this.paperEnrolleeReturneeFormState.form,
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
      this.careSettingsForm,
      this.regulatoryFormState.form,
      // TODO commented out until required to avoid it being validated
      // this.deviceProviderForm,
      this.oboSitesForm,
      this.remoteAccessLocationsForm,
      this.selfDeclarationForm
    ];
  }

  /**
  * @description
  * Check that all constituent forms are valid for submission.
  */
  public get isValidSubmission(): boolean {
    return this.isValid && this.hasCertificateOrOboSite();
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

    this.paperEnrolleeReturneeFormState = new PaperEnrolleeReturneeFormState(this.fb);
    this.bceidDemographicFormState = new BceidDemographicFormState(this.fb, this.formUtilsService);
    this.bcscDemographicFormState = new BcscDemographicFormState(this.fb, this.formUtilsService);
    this.regulatoryFormState = new RegulatoryFormState(this.fb, this.configService);
    this.oboSitesForm = this.buildOboSitesForm();
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
    const { certifications, enrolleeDeviceProviders, unlistedCertifications } = enrolment;
    this.regulatoryFormState.patchValue({ certifications, enrolleeDeviceProviders, unlistedCertifications });

    const {
      careSettings,
      enrolleeHealthAuthorities,
      oboSites,
      enrolleeRemoteUsers,
      remoteAccessSites,
      remoteAccessLocations,
      selfDeclarations,
      profileCompleted,
      requireRedoSelfDeclaration,
    } = enrolment;
    this.patchCareSettingsForm({ careSettings, enrolleeHealthAuthorities });
    this.patchOboSitesForm(oboSites);
    this.patchRemoteAccessForm({ enrolleeRemoteUsers, remoteAccessSites });
    this.patchRemoteAccessLocationsForm(remoteAccessLocations);
    if (!requireRedoSelfDeclaration) {
      this.patchSelfDeclarations({ profileCompleted, selfDeclarations });
    }

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
    const selfDeclarationsFormData = this.selfDeclarationForm.getRawValue();
    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED,
      hasRegistrationSuspendedDeviceProvider: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED_DEVICE_PROVIDER,
    };


    return Object.keys(selfDeclarationsTypes)
      .reduce((sds: SelfDeclaration[], sd: string) => {
        sds.push(
          new SelfDeclaration(
            selfDeclarationsTypes[sd],
            selfDeclarationsFormData[`${sd}Details`],
            selfDeclarationsFormData[`${sd}DocumentGuids`],
            this.enrolleeId,
            selfDeclarationsFormData[sd],
          )
        );
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
    // eslint-disable-next-line prefer-const
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

  private buildAccessForm(): UntypedFormGroup {
    return this.fb.group({
      accessCode: ['', [
        Validators.required,
        Validators.pattern(/^lobster$/)
      ]]
    });
  }

  private buildIdentityDocumentForm(): UntypedFormGroup {
    return this.fb.group({
      identificationDocumentGuid: [null, [Validators.required]]
    });
  }

  public buildOboSitesForm(): UntypedFormGroup {
    return this.fb.group({
      // lastUpdateDatetime is used to keep track if user has updated the obo sites or not.
      lastUpdatedDatetime: [null, []],
      oboSites: this.fb.array([]),
      communityHealthSites: this.fb.array([]),
      communityPharmacySites: this.fb.array([]),
      deviceProviderSites: this.fb.array([]),
      healthAuthoritySites: this.fb.group({})
    });
  }

  public buildOboSiteForm(): UntypedFormGroup {
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
      })
    });
  }

  public patchOboSitesForm(oboSites) {
    if (!Array.isArray(oboSites)) {
      oboSites = [];
    }

    const oboSitesFormArray = this.oboSitesForm.get('oboSites') as UntypedFormArray;
    const communityHealthSites = this.oboSitesForm.get('communityHealthSites') as UntypedFormArray;
    const communityPharmacySites = this.oboSitesForm.get('communityPharmacySites') as UntypedFormArray;
    const deviceProviderSites = this.oboSitesForm.get('deviceProviderSites') as UntypedFormArray;
    const healthAuthoritySites = this.oboSitesForm.get('healthAuthoritySites') as UntypedFormGroup;

    oboSitesFormArray.clear();
    communityHealthSites.clear();
    communityPharmacySites.clear();
    deviceProviderSites.clear();
    Object.keys(healthAuthoritySites.controls).forEach(healthAuthorityCode => healthAuthoritySites.removeControl(healthAuthorityCode));

    oboSites.forEach((s: OboSite) => {
      const careSettings = this.careSettingsForm.get('careSettings') as UntypedFormArray;
      //add existing obo job site back to the form only if the care setting is selected
      if (careSettings && (careSettings.value.length === 0 || careSettings.value.filter((c) => c.careSettingCode === s.careSettingCode).length > 0)) {

        const site = this.buildOboSiteForm() as UntypedFormGroup;
        site.patchValue(s);
        oboSitesFormArray.push(site);

        switch (s.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
            this.addNonHealthAuthorityOboSite(site, communityHealthSites);
            break;
          }
          case CareSettingEnum.COMMUNITY_PHARMACIST: {
            this.addNonHealthAuthorityOboSite(site, communityPharmacySites);
            break;
          }
          case CareSettingEnum.DEVICE_PROVIDER: {
            this.addNonHealthAuthorityOboSite(site, deviceProviderSites);
            break;
          }
          case CareSettingEnum.HEALTH_AUTHORITY: {
            //create separate site to avoid validator transfered to oboSitesFormArray
            const haSite = this.buildOboSiteForm();
            haSite.patchValue(s);
            this.addHealthAuthorityOboSite(haSite, healthAuthoritySites, s.healthAuthorityCode);
            break;
          }
        }
      }

    });
  }

  public buildRemoteAccessForm(): UntypedFormGroup {
    return this.fb.group({
      remoteAccessSites: this.fb.array([]),
      enrolleeRemoteUsers: this.fb.array([])
    });
  }

  public patchRemoteAccessForm({ enrolleeRemoteUsers, remoteAccessSites }: { enrolleeRemoteUsers, remoteAccessSites }) {
    if (!Array.isArray(enrolleeRemoteUsers)) {
      enrolleeRemoteUsers = [];
    }

    const enrolleeRemoteUsersFormArray = this.remoteAccessForm.get('enrolleeRemoteUsers') as UntypedFormArray;
    enrolleeRemoteUsersFormArray.clear();
    enrolleeRemoteUsers.forEach((eru: EnrolleeRemoteUser) => {
      const enrolleeRemoteUser = this.enrolleeRemoteUserFormGroup();
      enrolleeRemoteUser.patchValue(eru);
      enrolleeRemoteUsersFormArray.push(enrolleeRemoteUser);
    });

    if (!Array.isArray(remoteAccessSites)) {
      remoteAccessSites = [];
    }

    const remoteAccessSitesFormArray = this.remoteAccessForm.get('remoteAccessSites') as UntypedFormArray;
    remoteAccessSitesFormArray.clear();
    remoteAccessSites.forEach((ras: RemoteAccessSite) => {
      const remoteAccessSite = this.remoteAccessSiteFormGroup();
      // Add the vendors, and then patch the remaining fields
      const siteVendors = remoteAccessSite.get('siteVendors') as UntypedFormArray;
      ras.site.siteVendors
        .forEach(v => siteVendors.push(this.fb.group({ vendorCode: v.vendorCode })));

      remoteAccessSite.patchValue({
        enrolleeId: ras.enrolleeId,
        siteId: ras.siteId,
        doingBusinessAs: ras.site.doingBusinessAs
      });
      remoteAccessSitesFormArray.push(remoteAccessSite);
    });
  }

  public enrolleeRemoteUserFormGroup(): UntypedFormGroup {
    return this.fb.group({
      enrolleeId: [null, []],
      remoteUserId: [null, []]
    });
  }

  public remoteAccessSiteFormGroup(): UntypedFormGroup {
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

  public buildRemoteAccessLocationsForm(): UntypedFormGroup {
    return this.fb.group({
      remoteAccessLocations: this.fb.array([])
    });
  }

  public patchRemoteAccessLocationsForm(remoteAccessLocations: RemoteAccessLocation[]) {
    if (!Array.isArray(remoteAccessLocations)) {
      remoteAccessLocations = [];
    }

    const remoteAccessLocationsFormArray = this.remoteAccessLocationsForm.get('remoteAccessLocations') as UntypedFormArray;
    remoteAccessLocationsFormArray.clear();
    remoteAccessLocations.forEach((ral: RemoteAccessLocation) => {
      const remoteAccessLocation = this.remoteAccessLocationFormGroup();
      remoteAccessLocation.patchValue(ral);
      remoteAccessLocationsFormArray.push(remoteAccessLocation);
    });
  }

  public remoteAccessLocationFormGroup(): UntypedFormGroup {
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

  private buildCareSettingsForm(): UntypedFormGroup {
    return this.fb.group({
      careSettings: this.fb.array([]),
      enrolleeHealthAuthorities: this.fb.array([])
    });
  }

  public buildCareSettingForm(code: number = null): UntypedFormGroup {
    return this.fb.group({
      careSettingCode: [code, [Validators.required]]
    });
  }

  public patchCareSettingsForm(
    { careSettings, enrolleeHealthAuthorities }: { careSettings: CareSetting[], enrolleeHealthAuthorities: EnrolleeHealthAuthority[] }
  ) {
    if (!Array.isArray(careSettings)) {
      careSettings = [];
    }

    const careSettingsFormArray = this.careSettingsForm.get('careSettings') as UntypedFormArray;
    careSettingsFormArray.clear();
    careSettings.forEach((s: CareSetting) => {
      const careSetting = this.buildCareSettingForm();
      careSetting.patchValue(s);
      careSettingsFormArray.push(careSetting);
    });

    if (!Array.isArray(enrolleeHealthAuthorities)) {
      enrolleeHealthAuthorities = [];
    }

    // Initialize Health Authority form even if it might not be used by end user:
    // Create checkboxes for each known Health Authority, according to order of Health Authority list.
    const enrolleeHealthAuthoritiesFormArray = this.careSettingsForm.get('enrolleeHealthAuthorities') as UntypedFormArray;
    enrolleeHealthAuthoritiesFormArray.clear();
    // Set value of checkboxes according to previous selections, if any
    this.configService.healthAuthorities.forEach(ha => {
      const checked = enrolleeHealthAuthorities.some(eha => ha.code === eha.healthAuthorityCode);
      enrolleeHealthAuthoritiesFormArray.push(this.buildEnrolleeHealthAuthorityFormControl(checked));
    });
  }

  public buildEnrolleeHealthAuthorityFormControl(checkState: boolean): UntypedFormControl {
    return this.fb.control(checkState);
  }

  public removeHealthAuthorities() {
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities') as UntypedFormArray;
    enrolleeHealthAuthorities.controls.forEach(checkbox => {
      checkbox.setValue(false);
    });
  }

  public removeRemoteAccess() {
    this.remoteAccessForm = this.buildRemoteAccessForm();
    this.remoteAccessLocationsForm = this.buildRemoteAccessLocationsForm();
  }

  private buildSelfDeclarationForm(): UntypedFormGroup {
    return this.fb.group({
      hasConviction: [null, [FormControlValidators.requiredBoolean]],
      hasConvictionDetails: [null, []],
      hasConvictionDocumentGuids: this.fb.array([]),
      hasRegistrationSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspendedDetails: [null, []],
      hasRegistrationSuspendedDocumentGuids: this.fb.array([]),
      hasRegistrationSuspendedDeviceProvider: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspendedDeviceProviderDetails: [null, []],
      hasRegistrationSuspendedDeviceProviderDocumentGuids: this.fb.array([]),
      hasDisciplinaryAction: [null, [FormControlValidators.requiredBoolean]],
      hasDisciplinaryActionDetails: [null, []],
      hasDisciplinaryActionDocumentGuids: this.fb.array([]),
      hasPharmaNetSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasPharmaNetSuspendedDetails: [null, []],
      hasPharmaNetSuspendedDocumentGuids: this.fb.array([])
    });
  }

  public patchSelfDeclarations(
    { selfDeclarations, profileCompleted }:
      { selfDeclarations: SelfDeclaration[], profileCompleted: boolean }
  ): void {
    const defaultValue = profileCompleted ? false : null;
    const selfDeclarationsTypes = {
      hasConviction: SelfDeclarationTypeEnum.HAS_CONVICTION,
      hasRegistrationSuspended: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED,
      hasRegistrationSuspendedDeviceProvider: SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED_DEVICE_PROVIDER,
      hasDisciplinaryAction: SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION,
      hasPharmaNetSuspended: SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED
    };
    const selfDeclarationForm = Object.keys(selfDeclarationsTypes)
      .reduce((sds, sd) => {
        const type = selfDeclarationsTypes[sd];
        const selfDeclarationDetails = selfDeclarations
          .find(esd => esd.selfDeclarationTypeCode === type)
          ?.selfDeclarationDetails;
        const adapted = {
          [sd]: (selfDeclarationDetails) ? true : defaultValue,
          [`${sd}Details`]: (selfDeclarationDetails) ? selfDeclarationDetails : null
        };
        return { ...sds, ...adapted };
      }, {});

    this.selfDeclarationForm.patchValue(selfDeclarationForm);
  }

  private buildAccessAgreementForm(): UntypedFormGroup {
    return this.fb.group({
      accessAgreementGuid: [
        '',
        []
      ]
    });
  }

  public addNonHealthAuthorityOboSite(siteForm: UntypedFormGroup, siteFormList: UntypedFormArray) {
    const siteName = siteForm.get('siteName') as UntypedFormControl;
    this.formUtilsService.setValidators(siteName, [Validators.required]);
    siteFormList.push(siteForm);
  }

  /**
   * @param haSiteForm - aka Health Authority Facility Form
   * @param healthAuthoritySites - a FormArray where each element, representing a Health Authority where the enrollee
   * works, contains a FormArray. This nested FormArray contains a FormGroup for each facility that the enrollee works
   * at, in that Health Authority
   */
  public addHealthAuthorityOboSite(haSiteForm: UntypedFormGroup, healthAuthoritySites: UntypedFormGroup, healthAuthorityCode?: number) {
    const facilityName = haSiteForm.get('facilityName') as UntypedFormControl;
    this.formUtilsService.setValidators(facilityName, [Validators.required]);
    const healthAuthorityControl = haSiteForm.get('healthAuthorityCode') as UntypedFormControl;
    this.formUtilsService.setValidators(healthAuthorityControl, [Validators.required]);
    let sitesOfHealthAuthority = healthAuthoritySites.get(String(healthAuthorityCode)) as UntypedFormArray;
    if (!sitesOfHealthAuthority) {
      sitesOfHealthAuthority = this.fb.array([]);
      sitesOfHealthAuthority.setValidators([FormArrayValidators.atLeast(1)]);
      healthAuthoritySites.setControl(String(healthAuthorityCode), sitesOfHealthAuthority);
    }
    sitesOfHealthAuthority.push(haSiteForm);
  }

  public removeUnselectedHAOboSites() {
    // Obo Sites need to be removed from two different collections
    const oboSites = this.oboSitesForm.get('oboSites') as UntypedFormArray;
    const healthAuthoritySites = this.oboSitesForm.get('healthAuthoritySites') as UntypedFormGroup;
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities') as UntypedFormArray;
    // If the checkbox for the health authority is not selected, remove the corresponding Obo Sites
    this.configService.healthAuthorities.forEach((healthAuthority, index) => {
      if (!enrolleeHealthAuthorities.at(index).value) {
        for (let i = oboSites.controls.length - 1; i >= 0; i--) {
          const oboSiteForm = oboSites.controls[i] as UntypedFormGroup;
          if (oboSiteForm.controls.healthAuthorityCode.value === healthAuthority.code) {
            oboSites.removeAt(i);
          }
        }
        healthAuthoritySites.removeControl(String(healthAuthority.code));
      }
    });
  }

  public removeDeviceProvider() {
    const { careSettings } = this.json;

    const isDeviceProvider = careSettings.some(cs => cs.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);
    if (!isDeviceProvider) {
      this.regulatoryFormState.certificationNumber.setValue(null);
      this.regulatoryFormState.deviceProviderId.setValue(null);
      this.regulatoryFormState.deviceProviderRoleCode.setValue(null);
    }
  }

  /**
   * Document Upload Helpers
   */

  public addSelfDeclarationDocumentGuid(control: UntypedFormArray, value: string) {
    control.push(this.fb.control(value));
  }

  public removeSelfDeclarationDocumentGuid(control: UntypedFormArray, documentGuid: string) {
    control.removeAt(control.value.findIndex((guid: string) => guid === documentGuid));
  }

  public clearSelfDeclarationDocumentGuids() {
    [
      'hasConvictionDocumentGuids',
      'hasRegistrationSuspendedDocumentGuids',
      'hasRegistrationSuspendedDeviceProviderDocumentGuids',
      'hasDisciplinaryActionDocumentGuids',
      'hasPharmaNetSuspendedDocumentGuids'
    ]
      .map((formArrayName: string) => this.selfDeclarationForm.get(formArrayName) as UntypedFormArray)
      .forEach((formArray: UntypedFormArray) => formArray.clear());
  }

  /**
   * @description
   * Check that at least one certificate or OBO site exists, and that each
   * chosen health authority has at least one site.
   */
  private hasCertificateOrOboSite(): boolean {
    const { certifications, oboSites, careSettings } = this.json;

    const isDeviceProvider = careSettings.some(cs => cs.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);

    // When you set certifications to 'None' there still exists an item in the
    // list, and this checks for the existence of at least one valid certification
    const hasCertification = certifications.some(c => c?.licenseNumber);
    const enrolleeHealthAuthorities = this.careSettingsForm.get('enrolleeHealthAuthorities').value as boolean[];
    const hasOboSiteForEveryChosenHealthAuth = enrolleeHealthAuthorities
      .every((wasChosen: boolean, healthAuthorityIndex: HealthAuthorityEnum) =>
        (wasChosen)
          ? oboSites.some(os => os.healthAuthorityCode === this.configService.healthAuthorities[healthAuthorityIndex].code)
          : true
      );

    return isDeviceProvider || hasCertification || (oboSites.length && hasOboSiteForEveryChosenHealthAuth);
  }
}
