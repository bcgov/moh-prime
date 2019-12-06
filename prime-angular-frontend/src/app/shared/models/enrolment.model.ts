import { Config } from '@config/config.model';
import { CollegeCertification } from '../../modules/enrolment/shared/models/college-certification.model';
import { Job } from '../../modules/enrolment/shared/models/job.model';
import { Organization } from '../../modules/enrolment/shared/models/organization.model';
import { Enrollee } from '../../modules/enrolment/shared/models/enrollee.model';
import { EnrolmentStatus } from './enrolment-status.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

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
  // Indicates whether this is the enrollees initial application
  profileCompleted: boolean;
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
  // Indicates whether this is the enrollees initial application
  profileCompleted: boolean;
}
