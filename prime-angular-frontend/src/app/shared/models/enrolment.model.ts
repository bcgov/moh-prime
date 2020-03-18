import { Enrollee } from '@shared/models/enrollee.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { Organization } from '@enrolment/shared/models/organization.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { Privilege } from '@enrolment/shared/models/privilege.model';
import { Admin } from '@auth/shared/models/admin.model';

// TODO incoming transitional Enrollee model, eventually will be Enrollee
export interface HttpEnrollee extends Enrollee {
  id?: number;
  displayId?: number;
  hpdid: string;
  appliedDate: string;
  approvedDate: string;
  expiryDate?: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  jobs: Job[];
  hasConviction: boolean;
  hasConvictionDetails: string;
  hasRegistrationSuspended: boolean;
  hasRegistrationSuspendedDetails: string;
  hasDisciplinaryAction: boolean;
  hasDisciplinaryActionDetails: string;
  hasPharmaNetSuspended: boolean;
  hasPharmaNetSuspendedDetails: string;
  organizations: Organization[];
  privileges: Privilege[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: AdjudicationNote;
  accessAgreementNote: AdjudicationNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Indicates enrollee has seen the collection notice
  collectionNoticeAccepted: boolean;
  // Always send an enrollee to manual adjudication
  alwaysManual: boolean;
  adjudicatorId: number;
  adjudicator: Admin;
}

// TODO outgoing enrolment model
export interface Enrolment {
  id?: number;
  displayId?: number;
  enrollee: Enrollee;
  appliedDate: string;
  approvedDate: string;
  expiryDate?: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  jobs: Job[];
  hasConviction: boolean;
  hasConvictionDetails: string;
  hasRegistrationSuspended: boolean;
  hasRegistrationSuspendedDetails: string;
  hasDisciplinaryAction: boolean;
  hasDisciplinaryActionDetails: string;
  hasPharmaNetSuspended: boolean;
  hasPharmaNetSuspendedDetails: string;
  organizations: Organization[];
  privileges: Privilege[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: AdjudicationNote;
  accessAgreementNote: AdjudicationNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Indicates enrollee has seen the collection notice
  collectionNoticeAccepted: boolean;
  // Always send an enrollee to manual adjudication
  alwaysManual: boolean;
  adjudicatorId: number;
  adjudicator: Admin;
}
