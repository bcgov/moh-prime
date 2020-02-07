import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';

import { FormControlValidators } from '@shared/validators/form-control.validators';
import { Enrolment } from '@shared/models/enrolment.model';
import { Job } from '../models/job.model';
import { Organization } from '../models/organization.model';
import { CollegeCertification } from '../models/college-certification.model';

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

  private enrolleeId: number;
  private userId: string;

  constructor(
    private fb: FormBuilder
  ) {
    this.demographicForm = this.buildDemographicForm();
    this.regulatoryForm = this.buildRegulatoryForm();
    this.deviceProviderForm = this.buildDeviceProviderForm();
    this.jobsForm = this.buildJobsForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.organizationForm = this.buildOrganizationsForm();
  }

  /**
   * Store the enrolment JSON and populate the enrolment form.
   */
  public set enrolment(enrolment: Enrolment) {
    this.enrolleeId = enrolment.id;
    this.userId = enrolment.enrollee.userId;

    this.patchEnrolment(enrolment);
  }

  /**
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
    }
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
      hasContactPhone: [{ value: false, disabled: true }, []],
      contactPhone: [{ value: null, disabled: true }, [FormControlValidators.phone]]
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

  public buildDeviceProviderForm(): FormGroup {
    return this.fb.group({
      deviceProviderNumber: [null, [
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]],
      isInsulinPumpProvider: [false, [FormControlValidators.requiredBoolean]]
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
}
