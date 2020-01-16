import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';

export interface EnrolmentProfileHistory {
  id: number;
  enrolleeId: number;
  profileSnapshot: Enrolment;
  createdDate: string;
}

export interface HttpEnrolleeProfileHistory {
  id: number;
  enrolleeId: number;
  profileSnapshot: HttpEnrollee;
  createdDate: string;
}
