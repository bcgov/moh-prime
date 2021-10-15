import { Address } from '@shared/models/address.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

export interface SatEnrollee extends BcscUser {
  id: number;
  phone: string | null;
  preferredFirstName?: string | null;
  preferredMiddleName?: string | null;
  preferredLastName?: string | null;
  physicalAddress?: Address | null;
  partyCertifications: CollegeCertification[];
  submittedDate: string;
}
