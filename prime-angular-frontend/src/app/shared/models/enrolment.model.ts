import { Config } from '@config/config.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { Organization } from '@enrolment/shared/models/organization.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';


// TODO incoming transitional Enrollee model, eventually will be Enrollee
export interface HttpEnrollee extends Enrollee {
  id?: number;
  appliedDate: string;
  approvedDate: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  jobs: Job[];
  hasConviction: boolean;
  hasConvictionDetails: string;
  hasRegistrationSuspended: boolean;
  hasRegistrationSuspendedDetails: boolean;
  hasDisciplinaryAction: boolean;
  hasDisciplinaryActionDetails: boolean;
  hasPharmaNetSuspended: boolean;
  hasPharmaNetSuspendedDetails: boolean;
  organizations: Organization[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  availableStatuses: Config<number>[];
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: AdjudicationNote;
  accessAgreementNote: AdjudicationNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Status hook for where the enrollee is in the initial enrolment
  progressStatus: ProgressStatus;
}

// TODO outgoing enrolment model
export interface Enrolment {
  id?: number;
  enrollee: Enrollee;
  appliedDate: string;
  approvedDate: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  jobs: Job[];
  hasConviction: boolean;
  hasConvictionDetails: string;
  hasRegistrationSuspended: boolean;
  hasRegistrationSuspendedDetails: boolean;
  hasDisciplinaryAction: boolean;
  hasDisciplinaryActionDetails: boolean;
  hasPharmaNetSuspended: boolean;
  hasPharmaNetSuspendedDetails: boolean;
  organizations: Organization[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  availableStatuses: Config<number>[];
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: AdjudicationNote;
  accessAgreementNote: AdjudicationNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Status hook for where the enrollee is in the initial enrolment
  progressStatus: ProgressStatus;
  // Indicates enrollee has seen collection notice
  collectionNoticeAccepted: boolean;
}
