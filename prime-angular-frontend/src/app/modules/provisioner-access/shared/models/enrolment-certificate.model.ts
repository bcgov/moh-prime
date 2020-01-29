import { Config } from '@config/config.model';
import { Privilege } from '@enrolment/shared/models/privilege.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';

export interface EnrolmentCertificate {
  // TODO duplicate of EnrolleeProfile
  firstName: string;
  middleName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  licensePlate: string;
  organizationTypes: Config<number>[];
  enrolmentCertificateNote: AdjudicationNote;
  transactions: Privilege[];
  userType: Privilege;
  canHaveOBOs: Privilege;
  expiryDate: string;
}
