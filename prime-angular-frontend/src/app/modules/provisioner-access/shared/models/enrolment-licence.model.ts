import { Config } from '@config/config.model';
import { AgreementTypeGroup } from '@shared/enums/agreement-type-group.enum';

export interface EnrolmentLicence {
  collegeLicenceNumber: string;
  collegeCode: number;
  pharmaNetId: string;
  practRefId: string;
}
