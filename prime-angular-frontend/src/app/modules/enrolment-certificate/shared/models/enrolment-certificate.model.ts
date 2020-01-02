import { Privilege } from '@enrolment/shared/models/privilege.model';
import { Organization } from '@enrolment/shared/models/organization.model';

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
}
