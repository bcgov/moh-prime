import { Address } from '@lib/models/address.model';

export interface DemographicForm {
  preferredFirstName?: string;
  preferredMiddleName?: string;
  preferredLastName?: string;
  physicalAddress?: Address;
  email: string;
  phone: string;
}
