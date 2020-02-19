import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';

import { LoggerService } from '@core/services/logger.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { Enrolment } from '@shared/models/enrolment.model';
import { Job } from '../models/job.model';
import { Organization } from '../models/organization.model';
import { CollegeCertification } from '../models/college-certification.model';
import { RouteStateService } from '@core/services/route-state.service';
import { RouterEvent } from '@angular/router';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

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
  public organizationForm: FormGroup;

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
    this.organizationForm = this.buildOrganizationsForm();

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
  public get enrolment() {
    const id = this.enrolleeId;
    const userId = this.userId;

    const profile = this.demographicForm.getRawValue();
    const regulatory = this.regulatoryForm.getRawValue();
    const deviceProvider = this.deviceProviderForm.getRawValue();
    const jobs = this.jobsForm.getRawValue();
    const selfDeclaration = this.selfDeclarationForm.getRawValue();
    const organization = this.organizationForm.getRawValue();

    return {
      id,
      enrollee: {
        userId,
        ...profile
      },
      ...regulatory,
      ...deviceProvider,
      ...jobs,
      ...selfDeclaration,
      ...organization
    };
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
      this.isOrganizationValid()
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

  public isOrganizationValid(): boolean {
    return this.organizationForm.valid;
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
      this.selfDeclarationForm.patchValue(enrolment);
      this.organizationForm.patchValue(enrolment);

      if (enrolment.organizations.length) {
        const organizations = this.organizationForm.get('organizations') as FormArray;
        organizations.clear();
        enrolment.organizations.forEach((o: Organization) => {
          const organization = this.buildOrganizationForm();
          organization.patchValue(o);
          organizations.push(organization);
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
      this.organizationForm,
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

  private buildOrganizationsForm(): FormGroup {
    return this.fb.group({
      organizations: this.fb.array([])
    });
  }

  public buildOrganizationForm(code: number = null): FormGroup {
    return this.fb.group({
      organizationTypeCode: [code, [Validators.required]]
    });
  }

  private buildSelfDeclarationForm(): FormGroup {
    return this.fb.group({
      hasConviction: [null, [FormControlValidators.requiredBoolean]],
      hasConvictionDetails: [null, []],
      hasRegistrationSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspendedDetails: [null, []],
      hasDisciplinaryAction: [null, [FormControlValidators.requiredBoolean]],
      hasDisciplinaryActionDetails: [null, []],
      hasPharmaNetSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasPharmaNetSuspendedDetails: [null, []]
    });
  }
}
