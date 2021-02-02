import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl, FormControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
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
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';

import { RegulatoryFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { BcscDemographicFormState } from '@enrolment/pages/bcsc-demographic/bcsc-demographic-form-state.class';
import { BceidDemographicFormState } from '@enrolment/pages/bceid-demographic/bceid-demographic-form-state.class';
import { HealthAuthorityFormState } from '@enrolment/pages/health-authority/health-authority-form-state';

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
  public healthAuthoritiesFormState: HealthAuthorityFormState;
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
    const { jobs, oboSites } = this.jobsForm.getRawValue();
    const { enrolleeRemoteUsers } = this.remoteAccessForm.getRawValue();
    const remoteAccessLocations = this.remoteAccessLocationsForm.getRawValue();
    const careSettings = this.careSettingsForm.getRawValue();

    const enrolleeHealthAuthorities = this.healthAuthoritiesFormState.json;

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
      jobs,
      oboSites,
      ...careSettings,
      enrolleeHealthAuthorities,
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
      this.careSettingsForm,
      this.healthAuthoritiesFormState.form
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
   * Check for the requirement of at least one certification, or one job.
   */
  public hasCertificateOrJob(): boolean {
    const jobs = this.jobsForm.get('jobs') as FormArray;
    const certifications = this.regulatoryFormState.certifications;
    // When you set certifications to 'None' there still exists an item in
    // the FormArray, and this checks for its existence
    return jobs.length || (certifications.length && certifications.value[0].licenseNumber);
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
    this.healthAuthoritiesFormState = new HealthAuthorityFormState(this.fb, this.configService);
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

    if (enrolment.jobs.length) {
      const jobs = this.jobsForm.get('jobs') as FormArray;
      jobs.clear();
      enrolment.jobs.forEach((j: Job) => {
        const job = this.buildJobForm();
        job.patchValue(j);
        jobs.push(job);
      });
    }

    if (enrolment.oboSites.length && enrolment.jobs.length) {
      const oboSites = this.jobsForm.get('oboSites') as FormArray;
      const communityHealthSites = this.jobsForm.get('communityHealthSites') as FormArray;
      const communityPharmacySites = this.jobsForm.get('communityPharmacySites') as FormArray;
      const healthAuthoritySites = this.jobsForm.get('healthAuthoritySites') as FormArray;

      oboSites.clear();
      communityHealthSites.clear();
      communityPharmacySites.clear();
      healthAuthoritySites.clear();

      enrolment.careSettings.forEach((careSetting: CareSetting) => {
        switch (careSetting.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
            communityHealthSites.setValidators([FormArrayValidators.atLeast(1)]);
            break;
          }
          case CareSettingEnum.COMMUNITY_PHARMACIST: {
            communityPharmacySites.setValidators([FormArrayValidators.atLeast(1)]);
            break;
          }
          case CareSettingEnum.HEALTH_AUTHORITY: {
            healthAuthoritySites.setValidators([FormArrayValidators.atLeast(1)]);
            break;
          }
        }
      });

      enrolment.oboSites.forEach((s: OboSite) => {
        const site = this.buildOboSiteForm();
        site.patchValue(s);
        oboSites.push(site);

        switch (s.careSettingCode) {
          case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
            const siteName = site.get('siteName') as FormControl;
            this.formUtilsService.setValidators(siteName, [Validators.required]);
            communityHealthSites.push(site);
            break;
          }
          case CareSettingEnum.COMMUNITY_PHARMACIST: {
            const siteName = site.get('siteName') as FormControl;
            this.formUtilsService.setValidators(siteName, [Validators.required]);
            communityPharmacySites.push(site);
            break;
          }
          case CareSettingEnum.HEALTH_AUTHORITY: {
            const facilityName = site.get('facilityName') as FormControl;
            this.formUtilsService.setValidators(facilityName, [Validators.required]);
            healthAuthoritySites.push(site);
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
    this.careSettingsForm.patchValue(enrolment);

    this.regulatoryFormState.patchValue(enrolment.certifications);
    this.healthAuthoritiesFormState.patchValue(enrolment.enrolleeHealthAuthorities);

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
      jobs: this.fb.array([]),
      oboSites: this.fb.array([]),
      communityHealthSites: this.fb.array([]),
      communityPharmacySites: this.fb.array([]),
      healthAuthoritySites: this.fb.array([])
    });
  }

  public buildJobForm(value: string = null): FormGroup {
    return this.fb.group({
      title: [value, [Validators.required]]
    });
  }

  public buildOboSiteForm(): FormGroup {
    return this.fb.group({
      careSettingCode: [null, []],
      siteName: [null, []],
      facilityName: [null, []],
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
      physicalAddress: [null, []],
      siteVendors: [null, []]
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
      careSettings: this.fb.array([])
    });
  }

  public buildCareSettingForm(code: number = null): FormGroup {
    return this.fb.group({
      careSettingCode: [code, [Validators.required]]
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
