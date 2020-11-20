import { AgreementType } from '@shared/enums/agreement-type.enum';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';

/**
 * @deprecated
 */
export interface EnrolmentSubmission {
  id: number;
  enrolleeId: number;
  profileSnapshot: Enrolment;
  agreementType?: AgreementType;
  createdDate: string;
}

// TODO rename to EnrolleeSubmission when EnrolmentSubmission removed
export interface HttpEnrolleeSubmission {
  id: number;
  enrolleeId: number;
  profileSnapshot: HttpEnrollee;
  agreementType?: AgreementType;
  createdDate: string;
}
