import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { FormControlValidators } from '@shared/validators/form-control.validators';

// TODO: pass in enrolment and build out form groups and arrays within state service
@Injectable({
  providedIn: 'root'
})
export class EnrolmentStateService {
  public profileForm: FormGroup;
  public contactForm: FormGroup;
  public professionalInfoForm: FormGroup;
  public selfDeclarationForm: FormGroup;
  public pharmaNetAccessForm: FormGroup;

  constructor(
    private fb: FormBuilder
  ) {
    this.profileForm = this.buildProfileForm();
    this.contactForm = this.buildContactForm();
    this.professionalInfoForm = this.buildProfessionalInfoForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.pharmaNetAccessForm = this.buildPharmaNetAccessForm();

    // TODO: patch enrolment with test data
    this.profileForm.patchValue(this.getRawEnrolment().enrollee);
    this.contactForm.patchValue(this.getRawEnrolment().enrollee);
    // TODO: working to populate the forms of an existing enrolment
    // this.professionalInfoForm.patchValue(this.getRawEnrolment());
    // this.selfDeclarationForm.patchValue(this.getRawEnrolment());
    // this.pharmaNetAccessForm.patchValue(this.getRawEnrolment());
  }

  public getEnrolment() {
    const profile = this.profileForm.getRawValue();
    const contact = this.contactForm.getRawValue();
    const professionalInfo = this.professionalInfoForm.getRawValue();
    const selfDeclaration = this.selfDeclarationForm.getRawValue();
    const pharmaNetAccess = this.pharmaNetAccessForm.getRawValue();

    return {
      enrollee: {
        ...profile,
        ...contact
      },
      ...professionalInfo,
      ...selfDeclaration,
      ...pharmaNetAccess
    };
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

  public getRawEnrolment() {
    return {
      enrollee: {
        firstName: 'Martin',
        middleName: 'Tudor',
        lastName: 'Pultz',
        preferredFirstName: 'Nitram',
        preferredMiddleName: 'Rodut',
        preferredLastName: 'Ztlup',
        dateOfBirth: '1977-09-22T00:00:00',
        physicalAddress: {
          country: 'Canada',
          province: 'British Columbia',
          street: '1502 Fairfield',
          city: 'Victoria',
          postal: 'M4E 2B6'
        },
        mailingAddress: {
          country: 'Canada',
          province: 'British Columbia',
          street: '1394 Oak Bay Ave.',
          city: 'Victoria',
          postal: 'M4E 2B6'
        },
        contactEmail: 'mtpultz@gmail.com',
        contactPhone: '2507782367',
        voicePhone: '2507782367',
        voiceExtension: '836'
      },
      hasCertification: true,
      certifications: [
        {
          id: 1,
          collegeCode: 'CPSBC',
          licenseNumber: '41234445',
          licenseCode: 'FULGENERAL',
          renewalDate: '2020-10-01T00:00:00',
          practiceCode: 'REMOTEPRAC'
        },
        {
          id: 2,
          collegeCode: 'CRNBC',
          licenseNumber: '54325277',
          licenseCode: 'FULSEPCLTY',
          renewalDate: '2020-10-01T00:00:00',
          practiceCode: 'NONE'
        }
      ],
      isDeviceProvider: true,
      deviceProviderNumber: '37938227',
      isInsulinPumpProvider: true,
      isAccessingPharmaNetOnBehalfOf: true,
      jobs: [
        {
          id: 1,
          title: 'Midwife'
        }
      ],
      hasConviction: true,
      hasRegistrationSuspended: true,
      hasDisciplinaryAction: true,
      hasPharmaNetSuspended: true,
      organizations: [
        {
          id: 1,
          name: 'Vancouver Island',
          organizationTypeCode: 'HEALTHAUTH',
          city: 'Victoria',
          startDate: '2010-10-01T00:00:00',
          endDate: '2021-10-01T00:00:00'
        },
        {
          id: 2,
          name: 'Shopper\'s Drug Mart',
          organizationTypeCode: 'PHARMACY',
          city: 'Victoria',
          startDate: '2010-10-01T00:00:00',
          endDate: '2021-10-01T00:00:00'
        },
      ]
    };
  }
}
