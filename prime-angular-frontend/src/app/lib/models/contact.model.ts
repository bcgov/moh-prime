import { Person } from '@lib/models/person.model';
import { Address } from '@lib/models/address.model';

export class Contact implements Person {
  public id?: number;

  public email: string;
  public firstName: string;
  public lastName: string;

  public mailingAddressId?: number;
  public mailingAddress?: Address;
  public physicalAddressId?: number;
  public physicalAddress: Address;
  public phone: string;
  public smsPhone?: string;

  public fax?: string;
  public jobRoleTitle: string;
}
