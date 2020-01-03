import { Config } from '@config/config.model';

export interface EnrolmentStatusReason {
  enrolmentId: number;
  statusReasonCode: number;
  statusReason: Config<number>;
  reasonNote: string;
}
