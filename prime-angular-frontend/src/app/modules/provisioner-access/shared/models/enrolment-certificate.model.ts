import { Config } from '@config/config.model';

export interface EnrolmentCertificate {
  // TODO duplicate of EnrolleeProfile
  firstName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  gpid: string;
  organizationTypes: Config<number>[];
  expiryDate: string;
}
