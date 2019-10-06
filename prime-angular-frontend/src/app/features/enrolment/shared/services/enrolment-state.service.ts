import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { BehaviorSubject } from 'rxjs';

import * as moment from 'moment';
import { FormControlValidators } from '@shared/validators/form-control.validators';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentStateService {
  // private profileForm$ = new BehaviorSubject<FormGroup>(null);
  // public profileForm = this.profileForm$.asObservable();

  public profileForm: FormGroup;
  public contactForm: FormGroup;
  public professionalInfoForm: FormGroup;
  public selfDeclarationForm: FormGroup;
  public pharmNetAccessForm: FormGroup;

  constructor(
    private fb: FormBuilder
  ) {
    // this.profileForm$.next(this.buildProfileForm());
    this.profileForm = this.buildProfileForm();
    this.contactForm = this.buildContactForm();
    this.professionalInfoForm = this.buildProfessionalInfoForm();
    this.selfDeclarationForm = this.buildSelfDeclarationForm();
    this.pharmNetAccessForm = this.buildPharmNetAccessForm();
  }

  private buildProfileForm(): FormGroup {
    return this.fb.group({
      firstName: [{ value: '', disabled: true }, [Validators.required]],
      middleName: [{ value: '', disabled: true }, []],
      lastName: [{ value: '', disabled: true }, [Validators.required]],
      dateOfBirth: [{ value: moment(), disabled: true }, []],
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
      id: [null, []],
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

  private buildPharmNetAccessForm(): FormGroup {
    return this.fb.group({
      organizations: this.fb.array([])
    });
  }

  private getEnrolment() {
    return {
      enrollee: {
        userId: '1234567890',
        firstName: 'Joe',
        middleName: 'Bob',
        lastName: 'Blow',
        preferredFirstName: 'Joey',
        preferredMiddleName: 'May',
        preferredLastName: 'Blue',
        dateOfBirth: '2000-10-01T00:00:00',
        physicalAddress: {
          country: 'Canada',
          province: 'BC',
          street: '123 Easy St',
          city: 'Victoria',
          postal: 'V0V0V0'
        },
        mailingAddress: {
          country: 'Canada',
          province: 'BC',
          street: '321 Easy St',
          city: 'Victoria',
          postal: 'V0V0V0'
        },
        contactEmail: 'test@email.com',
        contactPhone: '555-555-5555',
        voicePhone: '555-555-5555',
        voiceExtension: '555'
      },
      appliedDate: '2019-10-02T14:14:41.57099',
      approved: null,
      approvedReason: null,
      approvedDate: null,
      hasCertification: true,
      certifications: [
        {
          id: 1,
          enrolmentId: 1,
          collegeCode: 1,
          licenseNumber: '9100000',
          licenseCode: 1,
          renewalDate: '2020-10-01T00:00:00',
          practiceCode: 1
        }
      ],
      isDeviceProvider: null,
      deviceProviderNumber: 'string',
      isInsulinPumpProvider: true,
      isAccessingPharmaNetOnBehalfOf: true,
      jobs: [
        {
          id: 1,
          enrolmentId: 1,
          title: 'Some Job'
        }
      ],
      hasConviction: true,
      hasRegistrationSuspended: true,
      hasDisciplinaryAction: true,
      hasPharmaNetSuspended: true,
      organizations: [
        {
          id: 1,
          enrolmentId: 1,
          name: 'Some Organization',
          organizationTypeCode: 1,
          city: 'Victoria',
          startDate: '2010-10-01T00:00:00',
          endDate: null
        }
      ]
    }
  }
}
