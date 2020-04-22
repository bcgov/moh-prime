import { Address } from '@shared/models/address.model';
import { User } from '@auth/shared/models/user.model';

export class Party {
  id?: number;
  userId: string;
  addressId?: number;
  physicalAddressId?: number;
  physicalAddress: Address;
  hpdid: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string; // TODO why are we storing this?
  jobRoleTitle: string = null;
  email: string = null;
  phone: string = null;
  fax?: string = null;
  SMSPhone?: string = null;

  constructor(user: User) {
    this.userId = user.userId;
    this.physicalAddress = user.physicalAddress;
    this.hpdid = user.hpdid;
    this.firstName = user.firstName;
    this.lastName = user.lastName;
    this.dateOfBirth = user.dateOfBirth;
  }
}
