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
  gpid: string;
  organizationTypes: Config<number>[];
  expiryDate: string;
}
