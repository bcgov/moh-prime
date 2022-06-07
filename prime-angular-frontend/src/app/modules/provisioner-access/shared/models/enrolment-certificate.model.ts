import { Config } from '@config/config.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';

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
}
