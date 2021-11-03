import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { Address } from '@lib/models/address.model';

export interface Enrollee extends Omit<BcscUser, 'username'> {
  id?: number;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  mailingAddress?: Address;
  physicalAddress?: Address;
  phone: string;
  phoneExtension?: string;
  smsPhone?: string;
  gpid: string;
}
