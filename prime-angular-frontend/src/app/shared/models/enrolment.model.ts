import { Enrollee } from '@shared/models/enrollee.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { Privilege } from '@shared/models/privilege.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { Admin } from '@auth/shared/models/admin.model';
import { SelfDeclaration } from './self-declarations.model';
import { SelfDeclarationDocument } from './self-declaration-document.model';

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
  selfDeclarations: SelfDeclaration[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  enrolleeCareSettings: CareSetting[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  previousStatus: EnrolmentStatus;
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: AdjudicationNote;
  accessAgreementNote: AdjudicationNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Indicates enrollee has seen the collection notice
  collectionNoticeAccepted: boolean;
  // Always send an enrollee to manual adjudication
  alwaysManual: boolean;
  requestingRemoteAccess: boolean;
  adjudicatorId: number;
  adjudicator: Admin;
  base64QRCode: string;
}


/**
 * @deprecated
 */
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

  selfDeclarations: SelfDeclaration[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  careSettings: CareSetting[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  previousStatus: EnrolmentStatus;
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: AdjudicationNote;
  accessAgreementNote: AdjudicationNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Indicates enrollee has seen the collection notice
  collectionNoticeAccepted: boolean;
  // Always send an enrollee to manual adjudication
  alwaysManual: boolean;
  requestingRemoteAccess: boolean;
  adjudicatorId: number;
  adjudicator: Admin;
  base64QRCode: string;
}
