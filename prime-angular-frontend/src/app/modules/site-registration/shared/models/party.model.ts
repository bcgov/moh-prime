import { Address } from '@shared/models/address.model';
import { User } from '@auth/shared/models/user.model';

export class Party {
  id?: number;
  userId: string;
  addressId?: number;
  physicalAddressId?: number;
  physicalAddress: Address;
  mailingAddressId?: number;
  mailingAddress?: Address;
  hpdid: string;
  firstName: string;
  lastName: string;
  preferredFirstName?: string;
  preferredMiddleName?: string;
  preferredLastName?: string;
  dateOfBirth: string;
  jobRoleTitle: string = null;
  email: string = null;
  phone: string = null;
  fax?: string = null;
  smsPhone?: string = null;

  constructor(user: User) {
    this.userId = user.userId;
    this.physicalAddress = user.physicalAddress;
    this.hpdid = user.hpdid;
    this.firstName = user.firstName;
    this.lastName = user.lastName;
    this.dateOfBirth = user.dateOfBirth;
  }
}
