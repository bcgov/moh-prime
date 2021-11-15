import { Person } from '@lib/models/person.model';
import { Address } from '@lib/models/address.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

// TODO should implement or extend BcscUser
export class Party implements Person {
  public id?: number;

  public hpdid: string;
  public userId: string;
  public email: string = null;
  public firstName: string;
  public lastName: string;
  public givenNames: string;
  public dateOfBirth: string;
  public verifiedAddressId?: number;
  public verifiedAddress: Address;

  public preferredFirstName?: string = null;
  public preferredMiddleName?: string = null;
  public preferredLastName?: string = null;
  public mailingAddressId?: number;
  public mailingAddress?: Address;
  public physicalAddressId?: number;
  public physicalAddress: Address;
  public phone: string = null;
  public phoneExtension?: string = null;
  public smsPhone?: string = null;

  public fax?: string = null;
  public jobRoleTitle: string = null;

  // TODO should be a public static factory for instantiation
  constructor(user: BcscUser) {
    this.hpdid = user.hpdid;
    this.userId = user.userId;
    this.firstName = user.firstName;
    this.lastName = user.lastName;
    this.givenNames = user.givenNames;
    this.dateOfBirth = user.dateOfBirth;
    this.verifiedAddress = user.verifiedAddress;
  }
}
