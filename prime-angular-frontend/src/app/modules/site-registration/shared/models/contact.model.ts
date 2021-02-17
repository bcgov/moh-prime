import { Address } from '@shared/models/address.model';
import { Person } from '@registration/shared/models/person.model';

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
