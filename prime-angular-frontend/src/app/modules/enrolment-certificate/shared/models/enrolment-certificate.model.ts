import { Privilege } from '@enrolment/shared/models/privilege.model';

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
}
