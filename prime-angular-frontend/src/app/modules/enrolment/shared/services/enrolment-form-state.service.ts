import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

import { IdentityProvider } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentFormStateService extends AbstractFormState<Enrolment> {
  public accessForm: FormGroup;
  public identityDocumentForm: FormGroup;
  public bceidDemographicForm: FormGroup;
  public bcscDemographicForm: FormGroup;
  public regulatoryForm: FormGroup;
  public deviceProviderForm: FormGroup;
  public jobsForm: FormGroup;
  public remoteAccessForm: FormGroup;
  public remoteAccessLocationsForm: FormGroup;
  public selfDeclarationForm: FormGroup;
  public careSettingsForm: FormGroup;

  private identityProvider: IdentityProvider;
  private enrolleeId: number;
  private userId: string;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    private authService: AuthService
  ) {
    super(fb, routeStateService, logger, [...EnrolmentRoutes.enrolmentProfileRoutes()]);
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
    const profile = (this.identityProvider === IdentityProvider.BCEID)
      ? this.bceidDemographicForm.getRawValue()
      : this.bcscDemographicForm.getRawValue();
    const regulatory = this.regulatoryForm.getRawValue();
    const deviceProvider = this.deviceProviderForm.getRawValue();
    const jobs = this.jobsForm.getRawValue();
    const remoteAccess = this.remoteAccessForm.getRawValue();
    const remoteAccessLocations = this.remoteAccessLocationsForm.getRawValue();
    const careSettings = this.careSettingsForm.getRawValue();
    const selfDeclarations = this.convertSelfDeclarationsToJson();

    return {
      id,
      enrollee: {
        userId,
        ...profile
      },
      ...regulatory,
      ...deviceProvider,
      ...jobs,
      ...careSettings,
      ...remoteAccess,
      ...remoteAccessLocations,
      selfDeclarations
    };
  }

  /**
   * @description
   * Helper for getting a list of enrolment forms.
   */
  public get forms(): AbstractControl[] {
    return [
      ...ArrayUtils.insertIf(
        this.identityProvider === IdentityProvider.BCEID,
        // Purposefully omitted accessForm and identityDocumentForm
        // from the list of forms since they are used out of band
        this.bceidDemographicForm
      ),
      ...ArrayUtils.insertIf(
        this.identityProvider === IdentityProvider.BCSC,
        this.bcscDemographicForm
      ),
      this.regulatoryForm,
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
   * Check for the requirement of at least one certification, or one job.
   */
  public hasCertificateOrJob(): boolean {
    const jobs = this.jobsForm.get('jobs') as FormArray;
    const certifications = this.regulatoryForm.get('certifications') as FormArray;
    // When you set certifications to 'None' there still exists an item in
    // the FormArray, and this checks for its existence
    return jobs.length || (certifications.length && certifications.value[0].licenseNumber);
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  protected buildForms() {
    // The accessForm and identityDocumentForm are used out of band
    // compared to the other form groups
    this.accessForm = this.buildAccessForm();
    this.identityDocumentForm = this.buildIdentityDocumentForm();
    this.bceidDemographicForm = this.buildBceidDemographicForm();
    this.bcscDemographicForm = this.buildBcscDemographicForm();
    this.regulatoryForm = this.buildRegulatoryForm();
    this.deviceProviderForm = this.buildDeviceProviderForm();
    this.jobsForm = this.buildJobsForm();
    this.remoteAccessForm = this.buildRemoteAccessForm();
    this.remoteAccessLocationsForm = this.buildRemoteAccessLocationsForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.careSettingsForm = this.buildCareSettingsForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(enrolment: Enrolment) {
    if (!enrolment) {
      return;
    }

    (this.identityProvider === IdentityProvider.BCEID)
      ? this.bceidDemographicForm.patchValue(enrolment.enrollee)
      : this.bcscDemographicForm.patchValue(enrolment.enrollee);
    this.deviceProviderForm.patchValue(enrolment);

    if (enrolment.certifications.length) {
      const certifications = this.regulatoryForm.get('certifications') as FormArray;
      certifications.clear();
      enrolment.certifications.forEach((c: CollegeCertification) => {
        const certification = this.buildCollegeCertificationForm();
        certification.patchValue(c);
        certifications.push(certification);
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

    if (enrolment.enrolleeRemoteUsers.length) {
      const enrolleeRemoteUsers = this.remoteAccessForm.get('enrolleeRemoteUsers') as FormArray;
      enrolleeRemoteUsers.clear();
      enrolment.enrolleeRemoteUsers.forEach((eru: EnrolleeRemoteUser) => {
        const enrolleeRemoteUser = this.enrolleeRemoteUserFormGroup();
        enrolleeRemoteUser.patchValue(eru);
        enrolleeRemoteUsers.push(enrolleeRemoteUser);
      });
    }

    this.regulatoryForm.patchValue(enrolment);
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

    if (enrolment.careSettings.length) {
      const careSettings = this.careSettingsForm.get('careSettings') as FormArray;
      careSettings.clear();
      enrolment.careSettings.forEach((s: CareSetting) => {
        const careSetting = this.buildCareSettingForm();
        careSetting.patchValue(s);
        careSettings.push(careSetting);
      });
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

  private buildBceidDemographicForm(): FormGroup {
    return this.fb.group({
      preferredFirstName: [null, [Validators.required]],
      preferredMiddleName: [null, []],
      preferredLastName: [null, [Validators.required]],
      mailingAddress: this.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        useDefaults: ['countryCode']
      }),
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [FormControlValidators.numeric]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      smsPhone: [null, [FormControlValidators.phone]],
      dateOfBirth: [{ value: null, disabled: false }, [Validators.required]]
    });
  }

  private buildBcscDemographicForm(): FormGroup {
    return this.fb.group({
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      mailingAddress: this.buildAddressForm(),
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [FormControlValidators.numeric]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      smsPhone: [null, [FormControlValidators.phone]]
    });
  }

  public buildRegulatoryForm(): FormGroup {
    return this.fb.group({
      certifications: this.fb.array([]),
    });
  }

  public buildCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', []],
      // Validators are applied at the component-level when
      // fields are made visible to allow empty submissions
      licenseNumber: [null, []],
      licenseCode: [null, []],
      renewalDate: [null, []],
      practiceCode: [null, []]
    });
  }

  public buildJobsForm(): FormGroup {
    return this.fb.group({
      jobs: this.fb.array([]),
    });
  }

  public buildJobForm(value: string = null): FormGroup {
    return this.fb.group({
      title: [value, [Validators.required]]
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
      siteId: [null, []]
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
      physicalAddress: this.buildAddressForm({
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
