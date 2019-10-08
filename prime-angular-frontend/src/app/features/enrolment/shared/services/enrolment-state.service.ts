import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { FormControlValidators } from '@shared/validators/form-control.validators';
import { Enrolment } from '../models/enrolment.model';

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
      // TODO: patch form arrays properly within service, and pull out of components for reuse
      // TODO: create separate service for reuseable form create methods
      this.profileForm.patchValue(enrolment.enrollee);
      this.contactForm.patchValue(enrolment.enrollee);
      this.professionalInfoForm.patchValue(enrolment);
      this.selfDeclarationForm.patchValue(enrolment);
      this.pharmaNetAccessForm.patchValue(enrolment);
    }
  }

  private buildProfileForm(): FormGroup {
    return this.fb.group({
      firstName: [{ value: '', disabled: true }, [Validators.required]],
      middleName: [{ value: '', disabled: true }, []],
      lastName: [{ value: '', disabled: true }, [Validators.required]],
      dateOfBirth: [{ value: null, disabled: true }, []],
      preferredFirstName: ['', []],
      preferredMiddleName: ['', []],
      preferredLastName: ['', []],
      physicalAddress: this.fb.group({
        country: [{ value: '', disabled: true }, []],
        province: [{ value: '', disabled: true }, []],
        street: [{ value: '', disabled: true }, []],
        city: [{ value: '', disabled: true }, []],
        postal: [{ value: '', disabled: true }, []]
      }),
      mailingAddress: this.fb.group({
        country: ['', []],
        province: ['', []],
        street: ['', []],
        city: ['', []],
        postal: ['', []]
      })
    });
  }

  private buildContactForm(): FormGroup {
    return this.fb.group({
      voicePhone: ['', [FormControlValidators.phone]],
      voiceExtension: ['', [FormControlValidators.numeric]],
      hasContactEmail: [false, []],
      contactEmail: ['', [FormControlValidators.email]],
      hasContactPhone: [false, []],
      contactPhone: ['', [FormControlValidators.phone]]
    });
  }

  private buildProfessionalInfoForm(): FormGroup {
    return this.fb.group({
      hasCertification: [null, [FormControlValidators.requiredBoolean]],
      certifications: this.fb.array([]),
      isDeviceProvider: [null, [FormControlValidators.requiredBoolean]],
      deviceProviderNumber: ['', []],
      isInsulinPumpProvider: [null, [FormControlValidators.requiredBoolean]],
      isAccessingPharmaNetOnBehalfOf: [null, [FormControlValidators.requiredBoolean]],
      jobs: this.fb.array([]),
    });
  }

  private buildSelfDeclarationForm(): FormGroup {
    return this.fb.group({
      hasConviction: [null, [FormControlValidators.requiredBoolean]],
      convictionDetails: [null, []],
      hasRegistrationSuspended: [null, [FormControlValidators.requiredBoolean]],
      registrationSuspendedDetails: [null, []],
      hasDisciplinaryAction: [null, [FormControlValidators.requiredBoolean]],
      disciplinaryActionDetails: [null, []],
      hasPharmaNetSuspended: [null, [FormControlValidators.requiredBoolean]],
      pharmaNetSuspendedDetails: [null, []]
    });
  }

  private buildPharmaNetAccessForm(): FormGroup {
    return this.fb.group({
      organizations: this.fb.array([])
    });
  }

  // TODO: temporary test data for filling out an enrolment
  public getRawEnrolment(): Enrolment {
    return {
      id: 2,
      enrollee: {
        id: 2,
        userId: '99999999',
        firstName: 'Marty',
        middleName: 'Tudor',
        lastName: 'Pultz',
        preferredFirstName: null,
        preferredMiddleName: null,
        preferredLastName: null,
        dateOfBirth: '1977-09-22T00:00:00',
        physicalAddress: {
          country: 'Canada',
          province: 'British Columbia',
          street: '1502 Fairfield',
          city: 'Victoria',
          postal: 'M4E 2B6'
        },
        mailingAddress: null,
        contactEmail: null,
        contactPhone: null,
        voicePhone: null,
        voiceExtension: null
      },
      hasCertification: null,
      certifications: [],
      isDeviceProvider: null,
      deviceProviderNumber: null,
      isInsulinPumpProvider: null,
      isAccessingPharmaNetOnBehalfOf: null,
      jobs: [],
      hasConviction: null,
      hasConvictionDetails: null,
      hasRegistrationSuspended: null,
      hasRegistrationSuspendedDetails: null,
      hasDisciplinaryAction: null,
      hasDisciplinaryActionDetails: null,
      hasPharmaNetSuspended: null,
      hasPharmaNetSuspendedDetails: null,
      organizations: []
    };
  }
}
