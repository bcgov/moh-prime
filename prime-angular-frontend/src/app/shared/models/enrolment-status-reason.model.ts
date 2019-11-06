import { Config } from '@config/config.model';

export interface EnrolmentStatusReason {
  enrolmentId: number;
  statusCode: number;
  statusReasonCode: number;
  statusReason: Config<number>;
}
