import { Config } from '@config/config.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';
import { EnrolmentLicence } from './enrolment-licence.model';
import { Site } from '@registration/shared/models/site.model';
export interface EnrolmentCertificate {
  // TODO duplicate of EnrolleeProfile
  firstName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  gpid: string;
  careSettings: Config<number>[];
  expiryDate: string;
  group: AgreementTypeGroup;
  accessType: string;
  licences: EnrolmentLicence[];
  deviceProviderId: string;
  remoteAccessSite: Site[];
}
