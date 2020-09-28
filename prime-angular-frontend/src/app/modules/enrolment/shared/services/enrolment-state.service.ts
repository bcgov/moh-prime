import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { RouterEvent } from '@angular/router';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

// TODO refactor into enrolment service and enrolment form service
@Injectable({
  providedIn: 'root'
})
export class EnrolmentStateService {
  // TODO revisit access to form groups as service is refined, but
  // for now public, and then make into BehaviourSubject or use
  // asObservable, which would make them immutable
  public demographicForm: FormGroup;
  public regulatoryForm: FormGroup;
  public deviceProviderForm: FormGroup;
  public jobsForm: FormGroup;
  public selfDeclarationForm: FormGroup;
  public careSettingsForm: FormGroup;

  private patched: boolean;
  private enrolleeId: number;
  private userId: string;

  constructor(
    private fb: FormBuilder,
    private routeStateService: RouteStateService,
    private logger: LoggerService
  ) {
    this.demographicForm = this.buildDemographicForm();
    this.regulatoryForm = this.buildRegulatoryForm();
    this.deviceProviderForm = this.buildDeviceProviderForm();
    this.jobsForm = this.buildJobsForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.careSettingsForm = this.buildCareSettingsForm();

    // Initial state of the form is unpatched and ready for
    // enrolment information
    this.patched = false;

    // Listen for a route end that is outside of the enrollee
    // profile/overview, and reset the form model
    this.routeStateService.onNavigationEnd()
      .subscribe((event: RouterEvent) => {
        const route = event.url.slice(event.url.lastIndexOf('/') + 1);
        if (!EnrolmentRoutes.enrolmentProfileRoutes().includes(route)) {
          this.logger.info('RESET ENROLLEE FORM');
          this.forms.forEach((form: FormGroup) => form.reset());
          this.patched = false;
        }
      });
  }

  public get isPatched() {
    return this.patched;
  }

  /**
   * @description
   * Store the enrolment JSON and populate the enrolment form, which can
   * only be set more than once when explicitly forced.
   */
  public setEnrolment(enrolment: Enrolment, force: boolean = false) {
    if (!this.patched || force) {
      // Indicate that the form is patched, and may contain unsaved information
      this.patched = true;

      this.enrolleeId = enrolment.id;
      this.userId = enrolment.enrollee.userId;

      this.patchEnrolment(enrolment);
    }
  }

  /**
   * @description
   * Get the enrolment as JSON for submission.
   */
  public get enrolment(): Enrolment {
    const id = this.enrolleeId;
    const userId = this.userId;

    const profile = this.demographicForm.getRawValue();
    const regulatory = this.regulatoryForm.getRawValue();
    const deviceProvider = this.deviceProviderForm.getRawValue();
    const jobs = this.jobsForm.getRawValue();
    const careSettings = this.careSettingsForm.getRawValue();
    const selfDeclarations = this.generateSelfDeclarations();

    return {
      id,
      selfDeclarations,
      enrollee: {
        userId,
        ...profile
      },
      ...regulatory,
      ...deviceProvider,
      ...jobs,
      ...careSettings
    };
  }

  private generateSelfDeclarations(): SelfDeclaration[] {
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

  public get isDirty(): boolean {
    return this.forms.reduce(
      (isDirty: boolean, form: FormGroup) => isDirty || form.dirty, false
    );
  }

  public markAsPristine(): void {
    this.forms.forEach((form: FormGroup) => form.markAsPristine());
  }

  public isEnrolmentValid(): boolean {
    return (
      this.isProfileInfoValid() &&
      this.isRegulatoryValid() &&
      // TODO removed until after Community Practice
      // this.isDeviceProviderValid() &&
      this.isJobsValid() &&
      this.isSelfDeclarationValid() &&
      this.isCareSettingValid() &&
      this.hasRegOrJob()
    );
  }

  public isProfileInfoValid(): boolean {
    return this.demographicForm.valid;
  }

  public isRegulatoryValid(): boolean {
    return this.regulatoryForm.valid;
  }

  public isDeviceProviderValid(): boolean {
    return this.deviceProviderForm.valid;
  }

  public isJobsValid(): boolean {
    return this.jobsForm.valid;
  }

  public isSelfDeclarationValid(): boolean {
    return this.selfDeclarationForm.valid;
  }

  public isCareSettingValid(): boolean {
    return this.careSettingsForm.valid;
  }

  public hasRegOrJob(): boolean {
    const jobs = this.jobsForm.get('jobs') as FormArray;
    const certifications = this.regulatoryForm.get('certifications') as FormArray;
    // When you set cert to 'None' there still exists an item in FormArray, this checks for that state
    return jobs.length > 0 || (certifications.length > 0 && (certifications.value[0].licenseNumber !== null));
  }

  /**
   * @description
   * Patch the enrolment forms with the enrolment JSON.
   *
   * @param enrolment JSON for patching
   */
  private patchEnrolment(enrolment: Enrolment) {
    if (enrolment) {
      this.demographicForm.patchValue(enrolment.enrollee);
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

      this.regulatoryForm.patchValue(enrolment);
      this.jobsForm.patchValue(enrolment);

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
  }

  private get forms(): FormGroup[] {
    return [
      this.demographicForm,
      this.regulatoryForm,
      this.jobsForm,
      this.deviceProviderForm,
      this.careSettingsForm,
      this.selfDeclarationForm
    ];
  }

  private buildDemographicForm(): FormGroup {
    return this.fb.group({
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      mailingAddress: this.fb.group({
        countryCode: [{ value: null, disabled: false }, []],
        provinceCode: [{ value: null, disabled: false }, []],
        street: [{ value: null, disabled: false }, []],
        street2: [{ value: null, disabled: false }, []],
        city: [{ value: null, disabled: false }, []],
        postal: [{ value: null, disabled: false }, []]
      }),
      voicePhone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      voiceExtension: [null, [FormControlValidators.numeric]],
      // TODO temporarily made visible until SMS available
      hasContactEmail: [true, []],
      // TODO temporarily made required until SMS available
      contactEmail: [null, [Validators.required, FormControlValidators.email]],
      // TODO temporarily made optional until SMS available to collect phone numbers in advance
      hasContactPhone: [true, []],
      contactPhone: [null, [FormControlValidators.phone]]
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
