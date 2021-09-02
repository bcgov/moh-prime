import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';
import { IEnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { Address } from '@shared/models/address.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';

export class MockEnrolmentService implements IEnrolmentService {
  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
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
        lastName: faker.name.lastName(),
        givenNames: faker.name.firstName(),
        preferredFirstName: null,
        preferredMiddleName: null,
        preferredLastName: null,
        dateOfBirth: faker.date.past(2).toDateString(),
        verifiedAddress: new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode()),
        mailingAddress: new Address(),
        physicalAddress: new Address(),
        email: faker.internet.email(),
        smsPhone: faker.phone.phoneNumber(),
        phone: faker.phone.phoneNumber(),
        phoneExtension: null,
        gpid: null,
        hpdid: null,
      },
      appliedDate: null,
      approvedDate: null,
      expiryDate: null,
      certifications: [],
      enrolleeRemoteUsers: [],
      remoteAccessSites: [],
      remoteAccessLocations: [],
      deviceProviderNumber: null,
      isInsulinPumpProvider: null,
      oboSites: [],
      enrolleeHealthAuthorities: [],
      selfDeclarations: [
        {
          id: faker.random.number(),
          enrolleeId: faker.random.number(),
          selfDeclarationTypeCode: faker.random.number({ min: 1, max: 4 }), // SelfDeclarationTypeEnum
          selfDeclarationDetails: faker.lorem.words(10),
          documentGuids: []
        }
      ],
      selfDeclarationDocuments: [],
      identificationDocuments: [],
      careSettings: [
        {
          id: faker.random.number(),
          careSettingCode: 1
        }
      ],
      enrolmentStatuses: null,
      currentStatus: {
        id: faker.random.number(),
        enrolmentId,
        statusCode: null,
        status: {
          code: EnrolmentStatusEnum.EDITABLE,
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
        ],
        adjudicator: null,
        enrolmentStatusReference: null

      },
      previousStatus: {
        id: faker.random.number(),
        enrolmentId,
        statusCode: null,
        status: {
          code: EnrolmentStatusEnum.EDITABLE,
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
        ],
        adjudicator: null,
        enrolmentStatusReference: null
      },
      currentTOAStatus: null,
      assignedTOAType: null,
      hasNewestAgreement: false,
      enrolleeClassification: EnrolleeClassification.OBO,
      enrolmentCertificateNote: null,
      accessAgreementNote: null,
      profileCompleted: true,
      collectionNoticeAccepted: false,
      alwaysManual: false,
      adjudicatorId: null,
      adjudicator: null,
      base64QRCode: null,
      confirmed: false,
      requiresConfirmation: false,
      jobs: []
    });
  }

  public get enrolment$(): BehaviorSubject<Enrolment> {
    return this._enrolment;
  }

  public get enrolment(): Enrolment {
    return this._enrolment.value;
  }

  public get isInitialEnrolment(): boolean {
    return !!this.enrolment.expiryDate;
  }

  public get isProfileComplete(): boolean {
    return (this.enrolment) ? this.enrolment.profileCompleted : false;
  }

  public canRequestRemoteAccess(certifications: CollegeCertification[], careSettings: CareSetting[]): boolean {
    return true;
  }
}
