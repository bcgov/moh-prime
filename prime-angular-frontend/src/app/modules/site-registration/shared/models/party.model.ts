import { Address } from '@shared/models/address.model';
import { User } from '@auth/shared/models/user.model';
import { Person } from '@registration/shared/models/person.model';

export class Party implements Person {
  public id?: number;
  public userId: string;
  public addressId?: number;
  public hpdid: string;
  public firstName: string;
  public lastName: string;
  public preferredFirstName?: string = null;
  public preferredMiddleName?: string = null;
  public preferredLastName?: string = null;
  public dateOfBirth: string;
  public email: string = null;
  public phone: string = null;
  public fax?: string = null;
  public smsPhone?: string = null;
  public jobRoleTitle: string = null;
  public physicalAddressId?: number;
  public physicalAddress: Address;
  public mailingAddressId?: number;
  public mailingAddress?: Address;

  // TODO should be a public static factory
  constructor(user: User) {
    this.userId = user.userId;
    this.physicalAddress = user.physicalAddress;
    this.hpdid = user.hpdid;
    this.firstName = user.firstName;
    this.lastName = user.lastName;
    this.dateOfBirth = user.dateOfBirth;
  }
}
