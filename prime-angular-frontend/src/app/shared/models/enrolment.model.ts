import { Enrollee } from '@shared/models/enrollee.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { IdentificationDocument } from '@shared/models/identification-document.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';

import { Admin } from '@auth/shared/models/admin.model';
import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

// TODO incoming transitional Enrollee model, eventually will be Enrollee
export interface HttpEnrollee extends Enrollee {
  displayId?: number;
  appliedDate: string;
  approvedDate: string;
  expiryDate?: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  oboSites: OboSite[];
  enrolleeRemoteUsers: EnrolleeRemoteUser[];
  remoteAccessSites: RemoteAccessSite[];
  remoteAccessLocations: RemoteAccessLocation[];
  selfDeclarations: SelfDeclaration[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  identificationDocuments: IdentificationDocument[];
  enrolleeCareSettings: CareSetting[];
  enrolleeHealthAuthorities: EnrolleeHealthAuthority[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  previousStatus: EnrolmentStatus;
  currentTOAStatus: string;
  assignedTOAType: number;
  hasNewestAgreement: boolean;
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: EnrolleeNote;
  accessAgreementNote: EnrolleeNote;
  // Indicates enrollee has not completed all profile information
  profileCompleted: boolean;
  // Indicates enrollee has seen the collection notice
  collectionNoticeAccepted: boolean;
  // Always send an enrollee to manual adjudication
  alwaysManual: boolean;
  adjudicatorId: number;
  adjudicator: Admin;
  base64QRCode: string;
  gpid: string;
}

/**
 * @deprecated
 * With the Enrolent table being dropped this is artifact
 * that needs to be removed to reduce confusion.
 *
 * NOTE: Enrolment contains equivalent data when compared to
 * HttpEnrollee, but HttpEnrollee has the enrollee information
 * flattened within the root of the object versus the Enrolment
 * model having a enrollee key
 */
export interface Enrolment {
  id?: number; // Equivalent to enrollee.id
  displayId?: number;
  enrollee: Enrollee;
  appliedDate: string;
  approvedDate: string;
  expiryDate?: string;
  certifications: CollegeCertification[];
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  oboSites: OboSite[];
  enrolleeRemoteUsers: EnrolleeRemoteUser[];
  remoteAccessSites: RemoteAccessSite[];
  remoteAccessLocations: RemoteAccessLocation[];
  selfDeclarations: SelfDeclaration[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  identificationDocuments: IdentificationDocument[];
  careSettings: CareSetting[];
  enrolleeHealthAuthorities: EnrolleeHealthAuthority[];
  enrolmentStatuses: EnrolmentStatus[];
  currentStatus: EnrolmentStatus;
  previousStatus: EnrolmentStatus;
  currentTOAStatus: string;
  assignedTOAType: number;
  hasNewestAgreement: boolean;
  enrolleeClassification: EnrolleeClassification;
  enrolmentCertificateNote: EnrolleeNote;
  accessAgreementNote: EnrolleeNote;
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
  currentTOAStatus: string;
  assignedTOAType: number;
  previousStatus: EnrolmentStatus;
  hasNewestAgreement: boolean;
  adjudicatorIdir: string;
  alwaysManual: boolean;
  remoteAccess: boolean;
  careSettingCodes: number[];
  hasNotification: boolean;
  requiresConfirmation: boolean;
  confirmed: boolean;
}
