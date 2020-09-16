import { Config } from '@config/config.model';

import { EnrolmentStatusReason } from './enrolment-status-reason.model';
import { Adjudicator } from './adjudicator.model';
import { EnrolmentStatusReference } from './enrolment-status-reference.model';

export interface EnrolmentStatus {
  id: number;
  enrolmentId: number;
  statusCode: number;
  status: Config<number>;
  statusDate: string;
  enrolmentStatusReasons: EnrolmentStatusReason[];
  adjudicator: Adjudicator;
  enrolmentStatusReference: EnrolmentStatusReference;
}
