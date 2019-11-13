import * as faker from 'faker';

import { BehaviorSubject } from 'rxjs';

import { Config } from '@config/config.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { IEnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

export class MockEnrolmentService implements IEnrolmentService {
  // tslint:disable-next-line: variable-name
  private _enrolment: BehaviorSubject<Enrolment>;

  constructor() {
    // TODO default enrolment should be refactored into methods to provide enrolments with different statuses
    const enrolmentId = faker.random.number();
    this._enrolment = new BehaviorSubject<Enrolment>({
      id: enrolmentId,
      enrollee: null,
      appliedDate: null,
      approvedDate: null,
      certifications: null,
      deviceProviderNumber: null,
      isInsulinPumpProvider: null,
      jobs: null,
      hasConviction: null,
      hasConvictionDetails: null,
      hasRegistrationSuspended: null,
      hasRegistrationSuspendedDetails: null,
      hasDisciplinaryAction: null,
      hasDisciplinaryActionDetails: null,
      hasPharmaNetSuspended: null,
      hasPharmaNetSuspendedDetails: null,
      organizations: null,
      enrolmentStatuses: null,
      currentStatus: {
        enrolmentId,
        statusCode: null,
        status: {
          code: EnrolmentStatus.IN_PROGRESS,
          name: null
        },
        statusDate: null,
        isCurrent: null,
        enrolmentStatusReasons: [
          {
            enrolmentId,
            statusCode: null,
            statusReasonCode: null,
            statusReason: {
              code: null,
              name: faker.lorem.sentence(6)
            }
          }
        ]
      },
      availableStatuses: null
    });
  }

  public get enrolment$(): BehaviorSubject<Enrolment> {
    return this._enrolment;
  }

  public get enrolment(): Enrolment {
    return this._enrolment.value;
  }

  public get status(): Config<number> {
    return this._enrolment.value.currentStatus.status;
  }
}
