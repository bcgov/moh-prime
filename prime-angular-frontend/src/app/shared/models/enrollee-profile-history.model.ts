import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';

/**
 * @deprecated
 */
export interface EnrolmentProfileVersion {
  id: number;
  enrolleeId: number;
  profileSnapshot: Enrolment;
  createdDate: string;
}

// TODO rename to EnrolleeProfileVersion when EnrolmentProfileVersion removed
export interface HttpEnrolleeProfileVersion {
  id: number;
  enrolleeId: number;
  profileSnapshot: HttpEnrollee;
  createdDate: string;
}
