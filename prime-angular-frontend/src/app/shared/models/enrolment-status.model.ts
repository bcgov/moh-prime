import { EnrolmentStatusReason } from './enrolment-status-reason.model';
import { Config } from '@config/config.model';

export interface EnrolmentStatus {
  enrolmentId: number;
  statusCode: number;
  status: Config<number>;
  statusDate: string;
  enrolmentStatusReasons: EnrolmentStatusReason[];
}
