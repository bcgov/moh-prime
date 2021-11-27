import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReference } from '@shared/models/enrolment-status-reference.model';

export interface EnrolmentStatusAdmin {
  id: number;
  statusCode: number;
  statusDate: string;
  enrolmentStatusReasons: { statusReasonCode: number, reasonNote: string }[];
  enrolmentStatusReference: EnrolmentStatusReference;
}
