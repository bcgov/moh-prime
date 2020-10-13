import { Enrollee } from '@shared/models/enrollee.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';

import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { IdentificationDocument } from './identification-document.model';

// TODO incoming transitional Enrollee model, eventually will be Enrollee
export interface HttpEnrollee extends Enrollee {
  displayId?: number;
  firstName: string;
  lastName: string;
  givenNames: string;
  appliedDate: string;
  approvedDate: string;
  expiryDate?: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  jobs: Job[];
  enrolleeRemoteUsers: EnrolleeRemoteUser[];
  selfDeclarations: SelfDeclaration[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  identificationDocuments: IdentificationDocument[];
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
  enrolleeRemoteUsers: EnrolleeRemoteUser[];
  selfDeclarations: SelfDeclaration[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  identificationDocuments: IdentificationDocument[];
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
  adjudicatorId: number;
  adjudicator: Admin;
  base64QRCode: string;
}

export interface EnrolleeListViewModel {
  id: number;
  displayId: number;
  firstName: string;
  lastName: string;
  givenNames: string;
  appliedDate: string;
  approvedDate: string;
  expiryDate: string;
  currentStatusCode: number;
  previousStatus: EnrolmentStatus;
  adjudicatorIdir: string;
  alwaysManual: boolean;
}
