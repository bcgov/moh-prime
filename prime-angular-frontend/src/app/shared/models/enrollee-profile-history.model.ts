import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';

export interface EnrolmentProfileVersion {
  id: number;
  enrolleeId: number;
  profileSnapshot: Enrolment;
  createdDate: string;
}

export interface HttpEnrolleeProfileVersion {
  id: number;
  enrolleeId: number;
  profileSnapshot: HttpEnrollee;
  createdDate: string;
}
