import { Address } from '@lib/models/address.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

export interface SatEnrollee extends BcscUser {
  id?: number;
  phone: string | null;
  partyCertifications: CollegeCertification[];
  submittedDate: string;
}
