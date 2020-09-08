import { Address } from '@shared/models/address.model';
import { Person } from '@registration/shared/models/person.model';

export class Contact implements Person {
  public id?: number;
  public firstName: string;
  public lastName: string;
  public jobRoleTitle: string;
  public email: string;
  public phone: string;
  public fax?: string;
  public smsPhone?: string;
  public physicalAddressId?: number;
  public physicalAddress: Address;
  public mailingAddressId?: number;
  public mailingAddress?: Address;
}
