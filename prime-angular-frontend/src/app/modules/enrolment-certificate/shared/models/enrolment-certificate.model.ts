import { Privilege } from '@enrolment/shared/models/privilege.model';
import { Organization } from '@enrolment/shared/models/organization.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';

export interface EnrolmentCertificate {
  // TODO duplicate of EnrolleeProfile
  firstName: string;
  middleName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  dateOfBirth: string;
  licensePlate: string;
  privileges: Privilege[];
  organizations: Organization[];
  // TODO should be an enum
  organizationTypes: number;
  enrolmentCertificateNote: AdjudicationNote;
}
