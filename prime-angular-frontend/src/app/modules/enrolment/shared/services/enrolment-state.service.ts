import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';

import { FormControlValidators } from '@shared/validators/form-control.validators';
import { Enrolment } from '@shared/models/enrolment.model';
import { Job } from '../models/job.model';
import { Organization } from '../models/organization.model';
import { CollegeCertification } from '../models/college-certification.model';

// TODO: revisit state service when API is flushed out more in next sprint
@Injectable({
  providedIn: 'root'
})
export class EnrolmentStateService {
  // TODO: revisit access to form groups as service is refined, but for now public
  // TODO: make into BehaviourSubject or asObservable, which would possibly make it immutable
  public profileForm: FormGroup;
  public contactForm: FormGroup;
  public professionalInfoForm: FormGroup;
  public selfDeclarationForm: FormGroup;
  public pharmaNetAccessForm: FormGroup;

  private enrolmentId: number;
  private enrolleeId: number;
  private userId: string;

  constructor(
    private fb: FormBuilder
  ) {
    this.profileForm = this.buildProfileForm();
    this.contactForm = this.buildContactForm();
    this.professionalInfoForm = this.buildProfessionalInfoForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.pharmaNetAccessForm = this.buildPharmaNetAccessForm();
  }

  /**
   * Store the enrolment JSON and populate the enrolment form.
   */
  public set enrolment(enrolment: Enrolment) {
    this.enrolmentId = enrolment.id;
    this.enrolleeId = enrolment.enrollee.id;
    this.userId = enrolment.enrollee.userId;

    this.patchEnrolment(enrolment);
  }

  /**
   * Get the enrolment as JSON for submission.
   */
  public get enrolment() {
    const id = this.enrolmentId;
    const enrolleeId = this.enrolleeId;
    const userId = this.userId;

    const profile = this.profileForm.getRawValue();
    const contact = this.contactForm.getRawValue();
    const professionalInfo = this.professionalInfoForm.getRawValue();
    const selfDeclaration = this.selfDeclarationForm.getRawValue();
    const pharmaNetAccess = this.pharmaNetAccessForm.getRawValue();

    return {
      id,
      enrollee: {
        id: enrolleeId,
        userId,
        ...profile,
        ...contact
      },
      ...professionalInfo,
      ...selfDeclaration,
      ...pharmaNetAccess
    };
  }

  public isEnrolmentValid(): boolean {
    return (
      this.isProfileInfoValid() &&
      this.isContactInfoValid() &&
      this.isProfessionalInfoValid() &&
      this.isSelfDeclarationValid() &&
      this.isPharmaNetAccessValid()
    );
  }

  public isProfileInfoValid(): boolean {
    return this.profileForm.valid;
  }

  public isContactInfoValid(): boolean {
    return this.contactForm.valid;
  }

  public isProfessionalInfoValid(): boolean {
    return this.professionalInfoForm.valid;
  }

  public isSelfDeclarationValid(): boolean {
    return this.selfDeclarationForm.valid;
  }

  public isPharmaNetAccessValid(): boolean {
    return this.pharmaNetAccessForm.valid;
  }

  /**
   * Patch the enrolment forms with the enrolment JSON.
   *
   * @param enrolment JSON for patching
   */
  private patchEnrolment(enrolment: Enrolment) {
    if (enrolment) {
      // TODO: create separate service for reuseable form create methods
      this.profileForm.patchValue(enrolment.enrollee);
      this.contactForm.patchValue(enrolment.enrollee);
      this.professionalInfoForm.patchValue(enrolment);

      if (enrolment.certifications.length) {
        const certifications = this.professionalInfoForm.get('certifications') as FormArray;
        certifications.clear();
        enrolment.certifications.forEach((c: CollegeCertification) => {
          const certification = this.buildCollegeCertificationForm();
          certification.patchValue(c);
          certifications.push(certification);
        });
      }

      if (enrolment.jobs.length) {
        const jobs = this.professionalInfoForm.get('jobs') as FormArray;
        jobs.clear();
        enrolment.jobs.forEach((j: Job) => {
          const job = this.buildJobForm();
          job.patchValue(j);
          jobs.push(job);
        });
      }

      this.selfDeclarationForm.patchValue(enrolment);
      this.pharmaNetAccessForm.patchValue(enrolment);

      if (enrolment.organizations.length) {
        const organizations = this.pharmaNetAccessForm.get('organizations') as FormArray;
        organizations.clear();
        enrolment.organizations.forEach((o: Organization) => {
          const organization = this.buildOrganizationForm();
          organization.patchValue(o);
          organizations.push(organization);
        });
      }
    }
  }

  private buildProfileForm(): FormGroup {
    return this.fb.group({
      userId: [{ value: null, disabled: true }, [Validators.required]],
      firstName: [{ value: null, disabled: true }, [Validators.required]],
      middleName: [{ value: null, disabled: false }, []],
      lastName: [{ value: null, disabled: true }, [Validators.required]],
      dateOfBirth: [{ value: null, disabled: false }, [Validators.required]],
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      physicalAddress: this.fb.group({
        country: [{ value: null, disabled: false }, [Validators.required]],
        province: [{ value: null, disabled: false }, [Validators.required]],
        street: [{ value: null, disabled: false }, [Validators.required]],
        city: [{ value: null, disabled: false }, [Validators.required]],
        postal: [{ value: null, disabled: false }, [Validators.required]]
      }),
      mailingAddress: this.fb.group({
        country: [{ value: null, disabled: false }, []],
        province: [{ value: null, disabled: false }, []],
        street: [{ value: null, disabled: false }, []],
        city: [{ value: null, disabled: false }, []],
        postal: [{ value: null, disabled: false }, []]
      })
    });
  }

  private buildContactForm(): FormGroup {
    return this.fb.group({
      voicePhone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      voiceExtension: [null, [FormControlValidators.numeric]],
      hasContactEmail: [false, []],
      contactEmail: [null, [FormControlValidators.email]],
      hasContactPhone: [false, []],
      contactPhone: [null, [FormControlValidators.phone]]
    });
  }

  private buildProfessionalInfoForm(): FormGroup {
    return this.fb.group({
      hasCertification: [null, [FormControlValidators.requiredBoolean]],
      certifications: this.fb.array([]),
      isDeviceProvider: [null, [FormControlValidators.requiredBoolean]],
      deviceProviderNumber: [null, [
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]],
      isInsulinPumpProvider: [null, [FormControlValidators.requiredBoolean]],
      isAccessingPharmaNetOnBehalfOf: [null, [FormControlValidators.requiredBoolean]],
      jobs: this.fb.array([]),
    });
  }

  public buildCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      id: [null, []],
      collegeCode: [null, [Validators.required]],
      licenseNumber: [null, [
        Validators.required,
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]],
      licenseCode: [null, [Validators.required]],
      renewalDate: [null, [Validators.required]],
      practiceCode: [null, []]
    });
  }

  public buildJobForm(value: string = null): FormGroup {
    return this.fb.group({
      id: [null, []],
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

  private buildPharmaNetAccessForm(): FormGroup {
    return this.fb.group({
      organizations: this.fb.array([])
    });
  }

  public buildOrganizationForm(): FormGroup {
    return this.fb.group({
      id: [null, []],
      organizationTypeCode: [null, [Validators.required]],
      name: [null, [Validators.required]],
      city: [null, [Validators.required]],
      startDate: [null, [Validators.required]],
      endDate: [null, []]
    });
  }
}
