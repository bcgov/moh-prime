import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Config } from '@config/config.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { IEnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { Address } from '@enrolment/shared/models/address.model';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';

export class MockEnrolmentService implements IEnrolmentService {
  // tslint:disable-next-line: variable-name
  private _enrolment: BehaviorSubject<Enrolment>;

  constructor() {
    // TODO default enrolment should be refactored into methods to provide enrolments with different statuses
    const enrolmentId = faker.random.number();
    this._enrolment = new BehaviorSubject<Enrolment>({
      id: enrolmentId,
      enrollee: {
        id: faker.random.number(),
        userId: faker.random.uuid(),
        firstName: faker.name.firstName(),
        middleName: faker.name.findName(),
        lastName: faker.name.lastName(),
        preferredFirstName: null,
        preferredMiddleName: null,
        preferredLastName: null,
        dateOfBirth: faker.date.past(2).toDateString(),
        physicalAddress: new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode()),
        mailingAddress: new Address(),
        contactEmail: faker.internet.email(),
        contactPhone: faker.phone.phoneNumber(),
        voicePhone: faker.phone.phoneNumber(),
        voiceExtension: null,
        licensePlate: null,
        expiryDate: null,
      },
      appliedDate: null,
      approvedDate: null,
      certifications: [],
      deviceProviderNumber: null,
      isInsulinPumpProvider: null,
      jobs: [],
      hasConviction: null,
      hasConvictionDetails: null,
      hasRegistrationSuspended: null,
      hasRegistrationSuspendedDetails: null,
      hasDisciplinaryAction: null,
      hasDisciplinaryActionDetails: null,
      hasPharmaNetSuspended: null,
      hasPharmaNetSuspendedDetails: null,
      organizations: [
        {
          id: faker.random.number(),
          organizationTypeCode: 1
        }
      ],
      privileges: [],
      enrolmentStatuses: null,
      currentStatus: {
        enrolmentId,
        statusCode: null,
        status: {
          code: EnrolmentStatus.IN_PROGRESS,
          name: null
        },
        statusDate: null,
        enrolmentStatusReasons: [
          {
            enrolmentId,
            statusReasonCode: null,
            statusReason: {
              code: null,
              name: faker.lorem.sentence(6)
            },
            reasonNote: null
          }
        ]
      },
      enrolleeClassification: EnrolleeClassification.OBO,
      enrolmentCertificateNote: null,
      accessAgreementNote: null,
      profileCompleted: true,
      progressStatus: ProgressStatus.STARTED,
      collectionNoticeAccepted: false,
      alwaysManual: false
    });
  }

  public get enrolment$(): BehaviorSubject<Enrolment> {
    return this._enrolment;
  }

  public get enrolment(): Enrolment {
    return this._enrolment.value;
  }

  public get isInitialEnrolment(): boolean {
    return this.enrolment.progressStatus !== ProgressStatus.FINISHED;
  }

  public get isProfileComplete(): boolean {
    return (this.enrolment) ? this.enrolment.profileCompleted : false;
  }
}
