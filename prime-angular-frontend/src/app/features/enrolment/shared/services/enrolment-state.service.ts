import { Injectable } from '@angular/core';
import { Enrolment } from '../models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentStateService {
  private enrolment: Enrolment;

  constructor() {
    // this.enrolment = this.getEnrolment();
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
